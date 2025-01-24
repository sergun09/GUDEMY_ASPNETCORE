using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class MinimumNumberOfRestaurants : IAuthorizationRequirement
{
    public int MinNumberOfRestaurants { get; }

    public MinimumNumberOfRestaurants(int minNumberOfRestaurants)
    {
        MinNumberOfRestaurants = minNumberOfRestaurants;
    }
}
