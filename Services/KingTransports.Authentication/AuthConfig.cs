using IdentityServer4;
using IdentityServer4.Models;

namespace KingTransports.Auth
{
    public class AuthConfig
    {
        public static IEnumerable<Client> Clients => new Client[]
        {
            new Client()
            {
                ClientId = "postman-backoffice-spa",
                AllowedGrantTypes = GrantTypes.Implicit,
                RequirePkce = false,
                RequireClientSecret = false,
                RedirectUris = { "https://oauth.pstmn.io/v1/callback", "https://www.getpostman.com/oauth2/callback"  },            
                AllowedScopes =
                {
                    "ticket.issue",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                },
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = true,
                RequireConsent = false
            },
            new Client()
            {
                ClientId = "postman-backoffice",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                RedirectUris = { "https://oauth.pstmn.io/v1/callback", "https://www.getpostman.com/oauth2/callback" },
                AllowedScopes =
                {
                    "ticketing",
                    "ticket.validate",
                    "roles",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = true,
                RequireConsent = false
            },
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope(name: "ticket.validate",   displayName: "Validate ticket"),
            new ApiScope(name: "ticket.issue",    displayName: "Issue ticket"),
        };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("ticketing", "Ticketing service")
            {
                UserClaims = { "role" },
                Scopes = { 
                    "ticket.validate", 
                    "ticket.issue"
                }
            }
        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
             new IdentityResources.OpenId(),
             new IdentityResources.Profile(),
             new IdentityResource("roles", new[] { "role" })
        };
    }
}
