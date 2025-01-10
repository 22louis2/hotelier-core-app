using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace hotelier_core_app.Service.Interface
{
    public interface ITokenService
    {
        string GetUserFullName(HttpRequest Request);

        string GetUserEmail(HttpRequest Request);

        string GenerateJSONWebToken(string fullname, string email, int roleId);

        JwtSecurityToken GetClaims(string token);

        string GetMacAddress(HttpRequest Request);

        int GetUserRole(HttpRequest Request);

        string GetJiraAuthorizationCode(HttpRequest request);
    }
}
