using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KingTransports.Common.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ScopesAndAdminOrAnyOfRolesAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private const string SCOPE_CLAIM_TYPE = "http://schemas.microsoft.com/identity/claims/scope";
        private readonly string[] _scopes;
        private readonly string[] _roles;

        public ScopesAndAdminOrAnyOfRolesAuthorizeAttribute(string[] scopes, string[] roles)
        {
            _scopes = scopes;
            _roles = roles;
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

            if (_roles == null)
            {
                return true;
            }

            return _roles.Any(principal.IsInRole);
        }

        private bool HasAllScopes(ClaimsPrincipal principal)
        {
            if (_scopes == null)
            {
                return true;
            }

            var claim = principal.FindFirst(SCOPE_CLAIM_TYPE);

            if (claim == null)
            {
                return false;
            }

            return !_scopes.Except(claim.Value.Split(' ')).Any();
        }
    }
}
