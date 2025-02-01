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
// [Authorize]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _accessor;
    
    public SubscriptionController(ISubscriptionService subscriptionService, ITokenService tokenService, IHttpContextAccessor accessor)
    {
        this._subscriptionService = subscriptionService;
        this._tokenService = tokenService;
        this._accessor = accessor;
    }
    
    // [Authorize(Policy = "DeveloperPolicy")]
    [HttpPost("create-plan")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BaseResponse))]
    public async Task<IActionResult> CreateSubscriptionPlan([FromBody] CreateSubscriptionPlanDto request)
    {
        AuditLog auditLog = new AuditLog
        {
            Action = UserAction.CreateSubscriptionPlan,
            DatePerformed = DateTime.UtcNow,
            PerformedBy = _tokenService.GetUserFullName(Request),
            IpAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP",
            // PerformerEmail = _tokenService.GetUserEmail(Request),
            PerformedAgainst = request.Name,
            // MacAddress = _tokenService.GetMacAddress(Request)
        };
        var response = await _subscriptionService.CreateSubscriptionPlanAsync(request, auditLog);
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse<SubscriptionPlanResponseDto>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(BaseResponse))]
    public async Task<IActionResult> GetSubscriptionPlanById(long id)
    {
        var response = await _subscriptionService.GetSubscriptionPlanByIdAsync(id);
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<SubscriptionPlanResponseDto>))]
    public async Task<IActionResult> GetAllSubscriptionPlans()
    {
        var response = await _subscriptionService.GetAllSubscriptionPlansAsync();
        return Ok(response);
    }
    
    // [Authorize(Policy = "DeveloperPolicy")]
    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BaseResponse))]
    public async Task<IActionResult> DeleteSubscriptionPlan(long id)
    {
        AuditLog auditLog = new AuditLog
        {
            Action = UserAction.DeleteSubscriptionPlan,
            DatePerformed = DateTime.UtcNow,
            PerformedBy = _tokenService.GetUserFullName(Request),
            IpAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP",
            PerformerEmail = _tokenService.GetUserEmail(Request),
            PerformedAgainst = id.ToString(),
            // MacAddress = _tokenService.GetMacAddress(Request)
        };
        var response = await _subscriptionService.DeleteSubscriptionPlanAsync(id, auditLog);
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
            PerformedBy = _tokenService.GetUserFullName(Request),
            IpAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP",
            // PerformerEmail = _tokenService.GetUserEmail(Request),
            PerformedAgainst = request.TenantId.ToString(),
            // MacAddress = _tokenService.GetMacAddress(Request)
        };
        var response = await _subscriptionService.AssignSubscriptionPlanToTenantAsync(request, auditLog);
        return response.Status ? Ok(response) : BadRequest(response);
    }
}