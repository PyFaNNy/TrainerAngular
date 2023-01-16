using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Trainer.IdentityServer;

public static class Configuration
{
    public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
            new Client
            {
                ClientId = "angular_client",
                ClientSecrets = { new Secret("angular_client".ToSha256()) },
                AllowedCorsOrigins = {"https://localhost:7075", "http://localhost:4200"},
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                AllowedScopes =
                {
                    "TrainerAPI"
                },
                AccessTokenLifetime = 360,
                AllowOfflineAccess = true
            }
        };

    public static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource> {
            new ApiResource("TrainerAPI")
        };

    public static IEnumerable<IdentityResource> GetIdentityResources() =>
        new List<IdentityResource> {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    
    public static IEnumerable<ApiScope> GetApiScopes()
    {
        yield return new ApiScope("TrainerAPI", "Trainer API");
    }
}