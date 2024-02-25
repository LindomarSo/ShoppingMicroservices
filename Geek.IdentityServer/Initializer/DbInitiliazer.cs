using Geek.IdentityServer.Configuration;
using Geek.IdentityServer.Model;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Geek.IdentityServer.Initializer;

public class DbInitiliazer(UserManager<ApplicationUser> user, RoleManager<IdentityRole> role) : IDbInitiliazer
{
    public void Initialize()
    {
        if (role.FindByNameAsync(IdentityConfiguration.Admin).Result is not null)
        {
            return;
        }

        role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
        role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

        var admin = new ApplicationUser
        {
            UserName = "lindomar-admin",
            Email = "lindomar-admin@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "+5561123412345",
            LastName = "Admin",
            FirstName = "Lindomar"
        };

        user.CreateAsync(admin, "1234").GetAwaiter().GetResult();
        user.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();

        user.AddClaimsAsync(admin,
        [
            new Claim(ClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
            new Claim(ClaimTypes.GivenName, $"{admin.FirstName}"),
            new Claim(JwtClaimTypes.FamilyName, $"{admin.LastName}"),
            new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin),
        ]).GetAwaiter().GetResult();

        var client = new ApplicationUser
        {
            UserName = "lindomar-client",
            Email = "lindomar-client@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "+5561123412345",
            LastName = "Client",
            FirstName = "Lindomar"
        };

        user.CreateAsync(client, "1234").GetAwaiter().GetResult();
        user.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();

        user.AddClaimsAsync(client,
        [
            new Claim(ClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
            new Claim(ClaimTypes.GivenName, $"{client.FirstName}"),
            new Claim(JwtClaimTypes.FamilyName, $"{client.LastName}"),
            new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client),
        ]).GetAwaiter().GetResult();
    }
}
