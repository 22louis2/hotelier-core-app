using hotelier_core_app.Model.Configs;
using hotelier_core_app.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace hotelier_core_app.Service.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<JwtConfig> _jwtConfig;

        public TokenService(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        /// <summary>
        /// Get user full name from token
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public string GetUserFullName(HttpRequest Request)
        {
            string fullName = string.Empty;
            var token = Request.Headers["Authorization"];

            if (token.Count != 0)
            {
                var authHeaderValue = AuthenticationHeaderValue.Parse(token.ToString());

                if (!string.IsNullOrWhiteSpace(token) && authHeaderValue.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase))
                {
                    return fullName;
                }

                var securityToken = GetClaims(token.ToString());
                if (securityToken != null)
                {
                    fullName = securityToken.Claims.FirstOrDefault(e => e.Type == "fullname").Value ?? string.Empty;
                }
            }
            return fullName;
        }

        /// <summary>
        /// Get user email from token
        /// </summary>
        /// <returns></returns>
        public string GetUserEmail(HttpRequest Request)
        {
            string email = string.Empty;
            var token = Request.Headers["Authorization"];
            var authHeaderValue = AuthenticationHeaderValue.Parse(token.ToString());

            if (!string.IsNullOrWhiteSpace(token) && authHeaderValue.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }
            JwtSecurityToken securityToken = GetClaims(token.ToString());
            if (securityToken.Claims.Any())
            {
                var emailClaim = securityToken.Claims.FirstOrDefault(e => e.Type == "email");
                if (emailClaim != null) {
                    email = emailClaim.Value;
                }
            }
            return email;
        }

        /// <summary>
        /// Generate jwt token
        /// </summary>
        /// <param name="fullname"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GenerateJSONWebToken(string fullname, string email, int roleId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Value.TokenKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim("fullname", fullname),
                new Claim("email", email),
                new Claim("role", roleId.ToString())
            };
            var token = new JwtSecurityToken(_jwtConfig.Value.TokenIssuer,
                _jwtConfig.Value.TokenIssuer,
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwtConfig.Value.TokenExpiryPeriod)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Get user token claim
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public JwtSecurityToken GetClaims(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {

                if (token.StartsWith("B"))
                {
                    token = token.Split(" ")[1];
                }
                var handler = new JwtSecurityTokenHandler();

                var decodedToken = handler.ReadToken(token) as JwtSecurityToken;

                return decodedToken ?? new JwtSecurityToken();
            }
            return new JwtSecurityToken();
        }

        /// <summary>
        /// Get user MacAddress from request Header
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public string GetMacAddress(HttpRequest Request)
        {
            return Request.Headers["MacAddress"].ToString();
        }

        public int GetUserRole(HttpRequest Request)
        {
            string roleName = string.Empty;
            var token = Request.Headers["Authorization"];
            var securityToken = GetClaims(token);
            if (securityToken != null)
            {
                roleName = securityToken.Claims.FirstOrDefault(e => e.Type == "role").Value;
            }
            int.TryParse(roleName, out int roleId);
            return roleId;
        }

        public string GetJiraAuthorizationCode(HttpRequest request)
        {
            return request.Headers["JiraAuthCode"];
        }
    }
}
