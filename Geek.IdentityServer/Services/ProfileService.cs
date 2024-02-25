using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Geek.IdentityServer.Model;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Geek.IdentityServer.Services;

public class ProfileService(UserManager<ApplicationUser> userManager, 
                            RoleManager<IdentityRole> roleManager, 
                            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipal) : IProfileService
{
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var id = context.Subject.GetSubjectId();
        var user = await userManager.FindByIdAsync(id);

        var userClaims = await userClaimsPrincipal.CreateAsync(user!);
        var claims = userClaims.Claims.ToList();
        claims.Add(new Claim(JwtClaimTypes.FamilyName, user?.LastName!));
        claims.Add(new Claim(JwtClaimTypes.GivenName, user?.FirstName!));

        if (userManager.SupportsUserRole)
        {
            var roles = await userManager.GetRolesAsync(user!);
            foreach (var role in roles) 
            {
                claims.Add(new Claim(JwtClaimTypes.Role, role));

                if (roleManager.SupportsRoleClaims)
                {
                    var identityRole = await roleManager.FindByNameAsync(role);
                    
                    if(identityRole != null)
                    {
                        claims.AddRange(await roleManager.GetClaimsAsync(identityRole));
                    }
                }
            }
        }

        context.IssuedClaims = claims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var id = context.Subject.GetSubjectId();
        var user = await userManager.FindByIdAsync(id);
        context.IsActive = user != null;
    }
}
