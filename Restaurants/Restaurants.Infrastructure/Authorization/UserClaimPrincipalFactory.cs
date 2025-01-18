using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using System.Security.Claims;

namespace Restaurants.Infrastructure.Authorization;

public class UserClaimPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
{
    public UserClaimPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) 
        : base(userManager, roleManager, options)
    {
    }

    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var claimsIdentity = await GenerateClaimsAsync(user);

        if (user.Nationality is not null)
            claimsIdentity.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));

        if (user.DateOfBirth is not null)
            claimsIdentity.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString()));

        return new ClaimsPrincipal(claimsIdentity);
    }
}
