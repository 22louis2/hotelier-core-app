using hotelier_core_app.Model.Configs;
using hotelier_core_app.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
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
        public string GetUserFullName(HttpRequest request)
        {
            return GetSingleClaimValue(ExtractSecurityToken(request), ClaimTypes.Name);
        }

        /// <summary>
        /// Get user email from token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetUserEmail(HttpRequest request)
        {
            return GetSingleClaimValue(ExtractSecurityToken(request), ClaimTypes.Email);
        }

        /// <summary>
        /// Get user roles from the token.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A list of user roles extracted from the token.</returns>
        public List<string> GetUserRoles(HttpRequest request)
        {
            return GetMultipleClaimValues(ExtractSecurityToken(request), ClaimTypes.Role);
        }

        /// <summary>
        /// Generate jwt token
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="email"></param>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        public string GenerateJSONWebToken(string fullName, string email, List<string> userRoles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Value.TokenKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim> {
                new(ClaimTypes.Name, fullName),
                new(ClaimTypes.Email, email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(_jwtConfig.Value.TokenIssuer,
                _jwtConfig.Value.TokenIssuer,
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwtConfig.Value.TokenExpiryPeriod)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Extracts the claims from a JWT token.
        /// </summary>
        /// <param name="token">The raw JWT token string.</param>
        /// <returns>A JwtSecurityToken object containing the token's claims.</returns>
        public JwtSecurityToken GetClaims(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("Token cannot be null or empty.");
            }

            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                var parts = token.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1)
                {
                    token = parts[1];
                }
            }

            var handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal;

            try
            {
                principal = ValidateToken(handler, token);
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException("Token validation failed.", ex);
            }

            var decodedToken = handler.ReadToken(token) as JwtSecurityToken;
            return decodedToken ?? throw new ArgumentException("Invalid JWT token format.");
        }

        /// <summary>
        /// Get user MacAddress from request Header
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetMacAddress(HttpRequest request)
        {
            return GetHeaderValue(request, "MacAddress");
        }

        /// <summary>
        /// Extracts claims from the token in the HTTP request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The JWT security token or null if invalid.</returns>
        private JwtSecurityToken ExtractSecurityToken(HttpRequest request)
        {
            var token = request.Headers.Authorization;
            if (string.IsNullOrWhiteSpace(token))
            {
                return new JwtSecurityToken();
            }

            var authHeaderValue = AuthenticationHeaderValue.Parse(token.ToString());
            if (authHeaderValue.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase))
            {
                return new JwtSecurityToken();
            }

            return GetClaims(token.ToString());
        }

        /// <summary>
        /// Extracts the value of a specific claim type from the token.
        /// </summary>
        /// <param name="securityToken"></param>
        /// <param name="claimType"></param>
        /// <returns>The first claim value for the specified claim type.</returns>
        private static string GetSingleClaimValue(JwtSecurityToken securityToken, string claimType)
        {
            return securityToken?.Claims.FirstOrDefault(c => c.Type == claimType)?.Value ?? string.Empty;
        }

        /// <summary>
        /// Extracts all values of a specific claim type from the token.
        /// </summary>
        /// <param name="securityToken"></param>
        /// <param name="claimType"></param>
        /// <returns>A list of claim values for the specified claim type.</returns>
        private static List<string> GetMultipleClaimValues(JwtSecurityToken securityToken, string claimType)
        {
            return securityToken?.Claims
                .Where(c => c.Type == claimType)
                .Select(c => c.Value)
                .ToList() ?? new List<string>();
        }

        /// <summary>
        /// Extracts the value of a specific header from the HTTP request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="headerName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private static string GetHeaderValue(HttpRequest request, string headerName)
        {
            if (request?.Headers == null || !request.Headers.TryGetValue(headerName, out StringValues value))
            {
                throw new ArgumentException($"{headerName} header is missing or the request is invalid.");
            }

            return value.ToString();
        }

        /// <summary>
        /// Validates the JWT token against the provided validation parameters.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private ClaimsPrincipal ValidateToken(JwtSecurityTokenHandler handler, string token)
        {
            return handler.ValidateToken(token, GetTokenValidationParameter(), out _);
        }

        private TokenValidationParameters GetTokenValidationParameter()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtConfig.Value.TokenIssuer,
                ValidAudience = _jwtConfig.Value.TokenIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Value.TokenKey))
            };
        }
    }
}
