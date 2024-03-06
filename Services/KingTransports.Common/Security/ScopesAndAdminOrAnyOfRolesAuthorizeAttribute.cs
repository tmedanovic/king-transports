using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KingTransports.Common.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ScopesAndAdminOrAnyOfRolesAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string[] Scopes;
        private string[] Roles;

        public ScopesAndAdminOrAnyOfRolesAuthorizeAttribute(string[] scopes, string[] roles)
        {
            Scopes = scopes;
            Roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorized = HasAllScopes(context.HttpContext.User) && HasAnyRole(context.HttpContext.User);

            if (!isAuthorized)
            {
                context.Result = new ForbidResult();
            }
        }

        private bool HasAnyRole(ClaimsPrincipal principal)
        {
            if (principal.IsInRole("admin"))
            {
                return true;
            }

            if (Roles == null)
            {
                return true;
            }

            return Roles.Any(principal.IsInRole);
        }

        private bool HasAllScopes(ClaimsPrincipal principal)
        {
            if (Scopes == null)
            {
                return true;
            }

            var userScopeClaims = principal.FindAll("scope");

            if (userScopeClaims == null || userScopeClaims.Count() == 0)
            {
                return false;
            }

            var userScopes = userScopeClaims.Select(x => x.Value).ToArray();
            return !Scopes.Except(userScopes).Any();
        }
    }
}
