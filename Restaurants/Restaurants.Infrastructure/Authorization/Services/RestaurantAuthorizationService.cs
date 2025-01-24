using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger, IUserContext userContext) :  IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, RessourceOperation ressourceOperation)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for Restaurant {RestaurantName}",
            user!.Email, ressourceOperation, restaurant.Name);

        if (ressourceOperation is RessourceOperation.Read || ressourceOperation is RessourceOperation.Create)
        {
            logger.LogInformation("Create/Read operation successfull");
            return true;
        }

        if (ressourceOperation is RessourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Admin User delete operation, successfull authorization");
            return true;
        }

        if (ressourceOperation is RessourceOperation.Delete || ressourceOperation is RessourceOperation.Update
            && user.Id == restaurant.OwnerId)
        {
            logger.LogInformation("Restaurant owner, successfull authorization");
            return true;
        }

        return false;
    }
}
