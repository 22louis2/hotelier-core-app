using AutoMapper;
using hotelier_core_app.Core.Constants;
using hotelier_core_app.Core.Enums;
using hotelier_core_app.Domain.Commands.Interface;
using hotelier_core_app.Domain.Helpers;
using hotelier_core_app.Model.DTOs.Request;
using hotelier_core_app.Model.DTOs.Response;
using hotelier_core_app.Model.Entities;
using hotelier_core_app.Service.Helpers;
using hotelier_core_app.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace hotelier_core_app.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IDBCommandRepository<AuditLog> _auditLogCommandRepository;
        private readonly IDBCommandRepository<Tenant> _tenantCommandRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly string _clientUrl;
        private const string HOTELIER_ADMIN = "Hotelier Admin";
        private const string HOTELIER_ADMIN_EMAIL = "admin@hotelier.com";

        public UserService(
            UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager, 
            SignInManager<ApplicationUser> signInManager,
            IDBCommandRepository<AuditLog> auditLogCommandRepository,
            IDBCommandRepository<Tenant> tenantCommandRepository,
            IEmailService emailService,
            IMapper mapper,
            IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _auditLogCommandRepository = auditLogCommandRepository;
            _tenantCommandRepository = tenantCommandRepository;
            _emailService = emailService;
            _mapper = mapper;
            _clientUrl = config.GetSection("Client:ClientURI").Value ?? string.Empty;
        }

        public async Task<BaseResponse> ActivateUser(ActivateUserRequestDTO model, AuditLog auditLog)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BaseResponse.Failure(ResponseMessages.UserDoesNotExist, ResponseStatusCode.UserDoesNotExist);
            }

            user.IsActive = true;
            string userStatus = user.Status;
            user.Status = UserStatus.Active.ToString();
            user.IsDeleted = false;

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(model.Role.ToString()))
                {
                    ApplicationRole newRole = _mapper.Map<ApplicationRole>(model);
                    newRole.CreationDate = DateTime.UtcNow;
                    newRole.CreatedBy = auditLog.PerformedBy;
                    await _roleManager.CreateAsync(newRole);
                }

                try
                {
                    var roleAssignmentResult = await _userManager.AddToRoleAsync(user, model.Role.ToString());
                    if (!roleAssignmentResult.Succeeded)
                    {
                        throw new Exception(HandleIdentityErrors(roleAssignmentResult).Message);
                    }
                }
                catch (Exception ex)
                {
                    user.Status = userStatus;
                    user.IsDeleted = true;
                    user.IsActive = false;
                    await _userManager.UpdateAsync(user);
                    return BaseResponse.Failure(ex.Message, ResponseStatusCode.OperationFailed);
                }

                _auditLogCommandRepository.Add(auditLog);

                return BaseResponse.Success(ResponseMessages.UserActivated, ResponseStatusCode.UserActivated);
            }

            return BaseResponse.Failure(ResponseMessages.OperationFailed, ResponseStatusCode.OperationFailed);
        }

        public async Task<BaseResponse> CreateUser(CreateUserRequestDTO model, AuditLog auditLog)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return BaseResponse.Failure(ResponseMessages.UserExist, ResponseStatusCode.UserExist);
            }

            ApplicationUser newUser = _mapper.Map<ApplicationUser>(model);
            newUser.CreationDate = DateTime.UtcNow;
            newUser.CreatedBy = HOTELIER_ADMIN;

            IdentityResult newUserResult = await _userManager.CreateAsync(newUser, model.Password);

            if (!newUserResult.Succeeded)
            {
                return HandleIdentityErrors(newUserResult);
            }

            if (!await _roleManager.RoleExistsAsync(model.Role.ToString()))
            {
                ApplicationRole newRole = _mapper.Map<ApplicationRole>(model);
                newRole.CreationDate = DateTime.UtcNow;
                newRole.CreatedBy = HOTELIER_ADMIN;
                await _roleManager.CreateAsync(newRole);
            }

            try
            {
                var roleAssignmentResult = await _userManager.AddToRoleAsync(newUser, model.Role.ToString());
                if (!roleAssignmentResult.Succeeded)
                {
                    throw new Exception(HandleIdentityErrors(roleAssignmentResult).Message);
                }
            }
            catch (Exception ex)
            {
                await _userManager.DeleteAsync(newUser);
                return BaseResponse.Failure(ex.Message, ResponseStatusCode.SQlException);
            }

            auditLog.PerformedBy = HOTELIER_ADMIN;
            auditLog.PerformerEmail = HOTELIER_ADMIN_EMAIL;
            _auditLogCommandRepository.SwitchProvider(DBProvider.SQL_Dapper);
            await _auditLogCommandRepository.AddAsync(auditLog);

            await SendEmailConfirmationAsync(newUser, model.Email);

            await CreateTenantAsync(model, newUser);

            return BaseResponse.Success(ResponseMessages.UserCreated, ResponseStatusCode.UserCreated);
        }

        public async Task<BaseResponse> DeactivateUser(DeactivateUserRequestDTO model, AuditLog auditLog)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BaseResponse.Failure(ResponseMessages.UserDoesNotExist, ResponseStatusCode.UserDoesNotExist);
            }

            if (model.Status.Equals(UserStatus.Active))
            {
                return BaseResponse.Failure();
            }

            user.IsActive = false;
            user.Status = model.Status.ToString();
            user.LastModifiedDate = DateTime.UtcNow;
            if (model.Status == UserStatus.Sacked || model.Status == UserStatus.Resigned) user.IsDeleted = true;

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                _auditLogCommandRepository.Add(auditLog);
                return BaseResponse.Success(ResponseMessages.UserDeactivated, ResponseStatusCode.UserDeactivated);
            }

            return BaseResponse.Failure(ResponseMessages.OperationFailed, ResponseStatusCode.OperationFailed);
        }

        public BaseResponse<List<ModuleGroupDTO>> GetAssignedModules(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<ApplicationUserDTO>> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<PageBaseResponse<List<ApplicationUserDTO>>> GetUsers(PageParamsDTO model)
        {
            throw new NotImplementedException();
        }

        public async Task<(BaseResponse<LoginResponseDTO>, string)> Login(UserLoginRequestDTO model, AuditLog auditLog)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == model.Email);

            if (user == null)
            {
                return (BaseResponse<LoginResponseDTO>.Failure(new LoginResponseDTO(),
                    ResponseMessages.UserDoesNotExist, ResponseStatusCode.UserDoesNotExist), string.Empty);
            }

            if (!user.IsActive)
            {
                return (BaseResponse<LoginResponseDTO>.Failure(new LoginResponseDTO(), 
                    ResponseMessages.UserInactive, ResponseStatusCode.UserInactive), string.Empty);
            }

            if (!user.EmailConfirmed)
            {
                return (BaseResponse<LoginResponseDTO>.Failure(new LoginResponseDTO(),
                    ResponseMessages.UserEmailNotConfirmed, ResponseStatusCode.UserEmailNotConfirmed), string.Empty);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (!signInResult.Succeeded)
            {
                return (BaseResponse<LoginResponseDTO>.Failure(new LoginResponseDTO(), 
                    ResponseMessages.InvalidCredential, ResponseStatusCode.InvalidCredential), string.Empty);
            }

            var userRole = await _userManager.GetRolesAsync(user);
            LoginResponseDTO data = new LoginResponseDTO
            {
                Email = user.Email ?? string.Empty,
                FullName = user.FullName,
                Picture = user.Picture ?? string.Empty,
                Roles = userRole.ToList()
            };

            string refreshToken = await GenerateRefreshTokenAndPersistData(user, auditLog);

            return (BaseResponse<LoginResponseDTO>.Success(data, ResponseMessages.LoginSuccessful, 
                ResponseStatusCode.LoginSuccessful), refreshToken);
        }

        public Task<BaseResponse> ReassignRole(EditUserRolesRequestDTO model, AuditLog auditLog)
        {
            throw new NotImplementedException();
        }

        public async Task<(BaseResponse<RefreshTokenResponseDTO>, string)> RefreshToken(RefreshTokenRequestDTO model, AuditLog auditLog)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == model.Email);
            if (user == null)
            {
                return (BaseResponse<RefreshTokenResponseDTO>.Failure(new RefreshTokenResponseDTO(), 
                    ResponseMessages.UserDoesNotExist, ResponseStatusCode.UserDoesNotExist), string.Empty);
            }

            if (user.RefreshToken == model.RefreshToken)
            {
                var userRole = await _userManager.GetRolesAsync(user);

                RefreshTokenResponseDTO data = new RefreshTokenResponseDTO()
                {
                    Email = user.Email?? string.Empty,
                    FullName = user.FullName,
                    Roles = userRole.ToList()
                };

                string refreshToken = await GenerateRefreshTokenAndPersistData(user, auditLog);

                return (BaseResponse<RefreshTokenResponseDTO>.Success(data, ResponseMessages.LoginSuccessful), refreshToken);
            }
            return (BaseResponse<RefreshTokenResponseDTO>.Failure(new RefreshTokenResponseDTO(), 
                ResponseMessages.CantVerifyRefreshToken, ResponseStatusCode.CantVerifyRefreshToken), string.Empty);
        }

        public Task<BaseResponse> UpdateUserDetail(EditUserDetailRequestDTO model, AuditLog auditLog)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> UpdateUserName(EditUserNameRequestDTO model, AuditLog auditLog)
        {
            throw new NotImplementedException();
        }

        #region private methods
        private async Task SendEmailConfirmationAsync(ApplicationUser user, string email)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var queryParams = new Dictionary<string, string?>
            {
                ["Email"] = email,
                ["Token"] = token
            };

            var confirmationLink = QueryHelpers.AddQueryString(_clientUrl + "emailconfirmation", queryParams);

            
            await _emailService.SendEmail(new SendEmailDTO(
                new List<string>() { email }, 
                "Email Confirmation", 
                confirmationLink, 
                null));
        }

        private async Task CreateTenantAsync(CreateUserRequestDTO model, ApplicationUser newUser)
        {
            var tenant = new Tenant
            {
                Name = model.HotelName,
                Description = $"Tenant for {model.HotelName}",
                SubscriptionPlanId = model.SubscriptionPlanId,
                CreatedBy = newUser.Id.ToString(),
                CreationDate = DateTime.UtcNow
            };

            tenant.Users.Add(newUser);

            await _tenantCommandRepository.AddAsync(tenant);
            await _tenantCommandRepository.SaveAsync();
        }

        private BaseResponse HandleIdentityErrors(IdentityResult result)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return BaseResponse.Failure(errors, ResponseStatusCode.IdentityError);
        }

        private async Task<string> GenerateRefreshTokenAndPersistData(ApplicationUser user, AuditLog auditLog)
        {
            string refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.LastModifiedDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            auditLog.PerformedBy = user.FullName;
            auditLog.PerformerEmail = user.Email ?? string.Empty;
            auditLog.PerformedAgainst = user.Email ?? string.Empty;
            auditLog.MacAddress = HashHelper.GenerateSHA256Hash(user.Email ?? string.Empty);

            _auditLogCommandRepository.Add(auditLog);

            return refreshToken;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Regex.Replace(Convert.ToBase64String(randomNumber), "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
        }
        #endregion
    }
}
