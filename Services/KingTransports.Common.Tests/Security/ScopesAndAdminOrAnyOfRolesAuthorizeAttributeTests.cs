using KingTransports.Common.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.Security.Claims;

namespace KingTransports.Common.Tests.Security;

public class ScopesAndAdminOrAnyOfRolesAuthorizeAttributeTests
{
    [Theory]
    [InlineData("api.read", null, null, "controlor", typeof(ForbidResult))]
    [InlineData("api.read", null, null, "admin", typeof(ForbidResult))]
    [InlineData("api.read, api.write", null, "api.read", "admin", typeof(ForbidResult))]
    [InlineData("api.read", "controlor", "api.read", "ticket-seller", typeof(ForbidResult))]
    [InlineData("api.read", "controlor", "api.read", "ticket-seller, controlor", null)]
    [InlineData(null, null, null, "admin", null)]
    public void ScopesAndAdminOrAnyOfRolesAuthorizeAttributeTestCases(string allowedScopes, string allowedRoles, string userScopes, string userRoles, Type result)
    {
        //var filter = new AuthorizationFilterContext(fakeActionContext, new List<IFilterMetadata>() { });
        var filter = new ScopesAndAdminOrAnyOfRolesAuthorizeAttribute(allowedScopes, allowedRoles);
        var user = GetTestUser(userScopes, userRoles);
        var context = BuildContext(user);
        filter.OnAuthorization(context);

        if(result !=  null)
        {
            Assert.Equal(result, context.Result.GetType());
        }
        else
        {
            Assert.Null(context.Result);
        }    
    }

    private AuthorizationFilterContext BuildContext(ClaimsPrincipal claimsPrincipal)
    {
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(a => a.User).Returns(claimsPrincipal);

        ActionContext fakeActionContext = new ActionContext(httpContextMock.Object, new RouteData(),
                                         new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());

        return new AuthorizationFilterContext(fakeActionContext, new List<IFilterMetadata>());
    }

    private ClaimsPrincipal GetTestUser(string scopes, string roles)
    {
        var identity = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.Name, "Test User")
        });

        if(scopes != null)
        {
            var _scopes = scopes.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var scope in _scopes)
            {
                identity.AddClaim(new Claim("scope", scope));
            }
        }

        if(roles != null)
        {
            var _roles = roles.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var role in _roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
        }

        var claimsPrincipal = new ClaimsPrincipal(identity);
        return claimsPrincipal;
    }
}