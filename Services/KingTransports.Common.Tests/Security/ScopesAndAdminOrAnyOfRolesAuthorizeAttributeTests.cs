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
    private const string SCOPE_CLAIM_TYPE = "http://schemas.microsoft.com/identity/claims/scope";

    [Theory]
    [InlineData(new string[] { "api.read" }, null, null, new string[] { "controlor" }, typeof(ForbidResult))]
    [InlineData(new string[] { "api.read" }, null, null, new string[] { "admin" }, typeof(ForbidResult))]
    [InlineData(new string[] { "api.read", "api.write" }, null, new string[] { "api.read" }, new string[] { "admin" }, typeof(ForbidResult))]
    [InlineData(new string[] { "api.read" }, new string[] { "controlor" }, new string[] { "api.read" }, new string[] { "ticket-seller" }, typeof(ForbidResult))]
    [InlineData(new string[] { "api.read" }, new string[] { "controlor" }, new string[] { "api.read" }, new string[] { "ticket-seller", "controlor" }, null)]
    [InlineData(null, null, null, new string[] { "admin" }, null)]
    public void ScopesAndAdminOrAnyOfRolesAuthorizeAttributeTestCases(string[] allowedScopes, string[] allowedRoles, string[] userScopes, string[] userRoles, Type result)
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

    private ClaimsPrincipal GetTestUser(string[] scopes, string[] roles)
    {
        var identity = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.Name, "Test User")
        });

        if(scopes != null)
        {
            identity.AddClaim(new Claim(SCOPE_CLAIM_TYPE, string.Join(' ', scopes)));
        }

        if(roles != null)
        {
            foreach(var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
        }

        var claimsPrincipal = new ClaimsPrincipal(identity);
        return claimsPrincipal;
    }
}