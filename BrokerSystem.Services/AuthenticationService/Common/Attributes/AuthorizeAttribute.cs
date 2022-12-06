using AuthenticationService.Common.Constants;
using AuthenticationService.Data;
using AuthenticationService.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthenticationService.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;

        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? Array.Empty<Role>();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var account = (Account?)context.HttpContext.Items[AuthConsants.ContextAccountItemKey];
            if (account == null || (_roles.Any() && !_roles.Contains(account.Role)))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
