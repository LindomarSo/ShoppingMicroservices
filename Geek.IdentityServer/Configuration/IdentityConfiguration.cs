using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Geek.IdentityServer.Configuration;

public static class IdentityConfiguration
{
    public const string Admin = nameof(Admin);
    public const string Client = nameof(Client);

    /// <summary>
    /// Resources são nomes de grupos de claims que podem ser requisitadas passando um parâmetro de scopo, basicamente são recursos a serem protegidos pelo identity server, como dados do usuário ou a própria API como um todo. São nomes únicos as quais podemos atribuir claims
    /// </summary>
    public static IEnumerable<IdentityResource> IdentityResources
        => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
        };

    /// <summary>
    /// São identificadores ou recursos que um client podem acessar. Existem dois tipos de scopos no nosso identity server o primeiro é o identity scope e o outro é o resource scope.
    /// O Identity scope contém o objeto com informações do próprio perfil.
    /// Um scope é usado por um client. Um Client é um component de software que solicita um token ao identity server.
    /// </summary>
    public static IEnumerable<ApiScope> ApiScopes
        => new List<ApiScope>
        {
            new ApiScope("geek_shopping", "GeekShopping Server"),
            new ApiScope("read", "Read data."),
            new ApiScope("write", "Write data."),
            new ApiScope("delete", "Delete data.")
        };

    /// <summary>
    /// Client da nossa aplicação GeekShopping Web  
    /// </summary>
    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("my_super_screct".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "read", "write", "profile" }
            },
            new Client
            {
                ClientId = "geek_shopping",
                ClientSecrets = { new Secret("my_super_screct".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:4430/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:4430/signout-callback-oidc" },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Profile,
                    "geek_shopping"
                }
            },
        };
}
