using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class MinimumNumberOfRestaurantsHandler(IRestaurantRepository restaurantRepository, IUserContext userContext) 
    : AuthorizationHandler<MinimumNumberOfRestaurants>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumNumberOfRestaurants requirement)
    {
        var currentUser = userContext.GetCurrentUser();

        var restaurants = await restaurantRepository.GetAllAsync();

        var userRestaurantCreated = restaurants.Count(r => r.OwnerId == currentUser.Id);

        if(userRestaurantCreated >= requirement.MinNumberOfRestaurants)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}
