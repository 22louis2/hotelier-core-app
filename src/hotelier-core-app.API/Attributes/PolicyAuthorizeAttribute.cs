using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace hotelier_core_app.API.Attributes
{
    public class PolicyAuthorizeAttribute : IAuthorizationFilter
    {
        private readonly string _policy;
        public PolicyAuthorizeAttribute(string policy) => _policy = policy;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new ForbidResult();
                return;
            }

            var authorizationService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();

            var policyResult = authorizationService.AuthorizeAsync(user, null, _policy).Result;

            if (!policyResult.Succeeded)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
