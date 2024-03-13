using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace KingTransports.Common.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ScopesAndAdminOrAnyOfRolesAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string Scopes;
        public string Roles;

        private string[] _scopes;
        private string[] _roles;

        public ScopesAndAdminOrAnyOfRolesAuthorizeAttribute()
        {
        }

        public ScopesAndAdminOrAnyOfRolesAuthorizeAttribute(string scopes)
        {
            Scopes = scopes;
        }

        public ScopesAndAdminOrAnyOfRolesAuthorizeAttribute(string scopes, string roles) : this(scopes)
        {
            Roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            IPrincipal user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

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

            // lets lazy split, do it only the first time and only when called
            if (_roles == null)
            {
                _roles = Roles.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            }
        
            return _roles.Any(principal.IsInRole);
        }

        private bool HasAllScopes(ClaimsPrincipal principal)
        {
            if (Scopes == null)
            {
                return true;
            }

            var userScopeClaims = principal.FindAll("scope");

            if (userScopeClaims == null || !userScopeClaims.Any())
            {
                return false;
            }

            var userScopes = userScopeClaims.Select(x => x.Value).ToArray();

            // lets lazy split, do it only the first time and only when called
            if (_scopes == null)
            {
                _scopes = Scopes.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            }

            return !_scopes.Except(userScopes).Any();
        }
    }
}
