using AuthenticationService.BLL.Contracts;
using AuthenticationService.Common.Configuration;
using AuthenticationService.Common.Constants;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace AuthenticationService.Middleware
{
    public class JwtMiddleware
    {
        private const string AuthHeader = "Authorization";

        private readonly RequestDelegate _next;
        private readonly SecuritySettings _securitySettings;

        public JwtMiddleware(RequestDelegate next, IOptions<SecuritySettings> securitySettings)
        {
            _next = next;
            _securitySettings = securitySettings.Value;
        }

        public async Task InvokeAsync(HttpContext context, IAccountsLogic accountsLogic)
        {
            var token = GetTokenFromRequest(context);

            if (token != null)
                await AttachAccountToContext(context, accountsLogic, token);

            await _next(context);
        }

        private static string? GetTokenFromRequest(HttpContext context)
        {
            return context.Request.Headers[HttpRequestHeader.Authorization.ToString()].FirstOrDefault()?.Split(" ").Last() ?? context.Request.Cookies[AuthConsants.JwtCookieKey];
        }

        private async Task AttachAccountToContext(HttpContext context, IAccountsLogic accountsLogic, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_securitySettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                if (int.TryParse(jwtToken.Claims.FirstOrDefault(x => x.Type == AuthConsants.IdClaimIdentifier)?.Value, out int accountId))
                {
                    context.Items[AuthConsants.ContextAccountItemKey] = await accountsLogic.GetAccountById(accountId);
                }
            }
            catch
            {
                // do nothing if jwt validation fails
            }
        }
    }
}
