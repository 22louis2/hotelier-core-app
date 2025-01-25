using System.Net;
using hotelier_core_app.API.Helpers;
using hotelier_core_app.Core.Constants;
using hotelier_core_app.Model.DTOs.Request;
using hotelier_core_app.Model.DTOs.Response;
using hotelier_core_app.Model.Entities;
using hotelier_core_app.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hotelier_core_app.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly ITokenService _tokenHelper;
    private readonly IHttpContextAccessor _accessor;

    RoleController(
        IRoleService roleService,
        ITokenService tokenHelper,
        IHttpContextAccessor accessor)
    {
        _roleService = roleService;
        _tokenHelper = tokenHelper;
        _accessor = accessor;
    }
    
    [HttpPost("create-role")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(BaseResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
    public async Task<IActionResult> CreateRole(CreateRoleRequestDto request)
    {
        AuditLog auditLog = new AuditLog
        {
            Action = UserAction.CreateUserRole,
            DatePerformed = DateTime.UtcNow,
            PerformedBy = _tokenHelper.GetUserFullName(Request),
            IpAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP",
            PerformerEmail = _tokenHelper.GetUserEmail(Request),
            PerformedAgainst = request.RoleName,
            MacAddress = _tokenHelper.GetMacAddress(Request)
        };

        var response = await _roleService.CreateRoleAsync(request, auditLog);
        return Ok(response);
    }
    
    [HttpPut("update-role")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(BaseResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
    public async Task<IActionResult> UpdateRole(UpdateRoleRequestDto request)
    {
        AuditLog auditLog = new AuditLog
        {
            Action = UserAction.EditUserRole,
            DatePerformed = DateTime.UtcNow,
            PerformedBy = _tokenHelper.GetUserFullName(Request),
            IpAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP",
            PerformerEmail = _tokenHelper.GetUserEmail(Request),
            PerformedAgainst = request.RoleName,
            MacAddress = _tokenHelper.GetMacAddress(Request)
        };

        var response = await _roleService.UpdateRoleAsync(request, auditLog);
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse<RoleResponseDto>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(BaseResponse))]
    public async Task<IActionResult> GetRoleById(long id)
    {
        var response = await _roleService.GetRoleByIdAsync(id);
        return Ok(response);
    }
    
    [HttpGet()]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PageBaseResponse<List<RoleResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(BaseResponse))]
    public async Task<IActionResult> GetAllRoles([FromQuery] GetRolesInputDTO input)
    {
        var response = await _roleService.GetAllRolesAsync(input);
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(BaseResponse))]
    public async Task<IActionResult> DeleteRole(long id)
    {
        AuditLog auditLog = new AuditLog
        {
            Action = UserAction.DeleteUserRole,
            DatePerformed = DateTime.UtcNow,
            PerformedBy = _tokenHelper.GetUserFullName(Request),
            IpAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP",
            PerformerEmail = _tokenHelper.GetUserEmail(Request),
            PerformedAgainst = $"Role ID: {id}",
            MacAddress = _tokenHelper.GetMacAddress(Request)
        };

        var response = await _roleService.DeleteRoleAsync(id, auditLog);
        return Ok(response);
    }
}
