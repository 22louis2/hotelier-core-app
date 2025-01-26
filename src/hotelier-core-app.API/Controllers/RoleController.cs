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
public class RoleController(
    IRoleService roleService,
    ITokenService tokenHelper,
    IHttpContextAccessor accessor)
    : ControllerBase
{
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
            PerformedBy = tokenHelper.GetUserFullName(Request),
            IpAddress = accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP",
            PerformerEmail = tokenHelper.GetUserEmail(Request),
            PerformedAgainst = request.RoleName,
            MacAddress = tokenHelper.GetMacAddress(Request)
        };

        var response = await roleService.CreateRoleAsync(request, auditLog);
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
            PerformedBy = tokenHelper.GetUserFullName(Request),
            IpAddress = accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP",
            PerformerEmail = tokenHelper.GetUserEmail(Request),
            PerformedAgainst = request.RoleName,
            MacAddress = tokenHelper.GetMacAddress(Request)
        };

        var response = await roleService.UpdateRoleAsync(request, auditLog);
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse<RoleResponseDto>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(BaseResponse))]
    public async Task<IActionResult> GetRoleById(long id)
    {
        var response = await roleService.GetRoleByIdAsync(id);
        return Ok(response);
    }
    
    [HttpGet()]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PageBaseResponse<List<RoleResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(BaseResponse))]
    public async Task<IActionResult> GetAllRoles([FromQuery] GetRolesInputDTO input)
    {
        var response = await roleService.GetAllRolesAsync(input);
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
            PerformedBy = tokenHelper.GetUserFullName(Request),
            IpAddress = accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP",
            PerformerEmail = tokenHelper.GetUserEmail(Request),
            PerformedAgainst = $"Role ID: {id}",
            MacAddress = tokenHelper.GetMacAddress(Request)
        };

        var response = await roleService.DeleteRoleAsync(id, auditLog);
        return Ok(response);
    }
}
