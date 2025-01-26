using System.Net;
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
public class SubscriptionController(
    ISubscriptionService subscriptionService, 
    ITokenService tokenHelper,
    IHttpContextAccessor accessor) : ControllerBase
{
    [HttpPost("create-plan")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BaseResponse))]
    public async Task<IActionResult> CreateSubscriptionPlan([FromBody] CreateSubscriptionPlanDto request)
    {
        AuditLog auditLog = new AuditLog
        {
            Action = UserAction.CreateSubscriptionPlan,
            DatePerformed = DateTime.UtcNow,
            PerformedBy = tokenHelper.GetUserFullName(Request),
            IpAddress = accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP",
            PerformerEmail = tokenHelper.GetUserEmail(Request),
            PerformedAgainst = request.Name,
            MacAddress = tokenHelper.GetMacAddress(Request)
        };
        var response = await subscriptionService.CreateSubscriptionPlanAsync(request, auditLog);
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse<SubscriptionPlanResponseDto>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(BaseResponse))]
    public async Task<IActionResult> GetSubscriptionPlanById(long id)
    {
        var response = await subscriptionService.GetSubscriptionPlanByIdAsync(id);
        return response.Status ? Ok(response) : NotFound(response);
    }
    
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<SubscriptionPlanResponseDto>))]
    public async Task<IActionResult> GetAllSubscriptionPlans()
    {
        var response = await subscriptionService.GetAllSubscriptionPlansAsync();
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BaseResponse))]
    public async Task<IActionResult> DeleteSubscriptionPlan(long id)
    {
        AuditLog auditLog = new AuditLog
        {
            Action = UserAction.DeleteSubscriptionPlan,
            DatePerformed = DateTime.UtcNow,
            PerformedBy = tokenHelper.GetUserFullName(Request),
            IpAddress = accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP",
            PerformerEmail = tokenHelper.GetUserEmail(Request),
            PerformedAgainst = id.ToString(),
            MacAddress = tokenHelper.GetMacAddress(Request)
        };
        var response = await subscriptionService.DeleteSubscriptionPlanAsync(id, auditLog);
        return response.Status ? Ok(response) : BadRequest(response);
    }
    
    [HttpPost("subscribe")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BaseResponse))]
    public async Task<IActionResult> AssignSubscriptionPlanToTenant([FromBody] AssignSubscriptionPlanDto request)
    {
        AuditLog auditLog = new AuditLog
        {
            Action = UserAction.ActivateSubscriptionPlan,
            DatePerformed = DateTime.UtcNow,
            PerformedBy = tokenHelper.GetUserFullName(Request),
            IpAddress = accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP",
            PerformerEmail = tokenHelper.GetUserEmail(Request),
            PerformedAgainst = request.TenantId.ToString(),
            MacAddress = tokenHelper.GetMacAddress(Request)
        };
        var response = await subscriptionService.AssignSubscriptionPlanToTenantAsync(request, auditLog);
        return response.Status ? Ok(response) : BadRequest(response);
    }
}