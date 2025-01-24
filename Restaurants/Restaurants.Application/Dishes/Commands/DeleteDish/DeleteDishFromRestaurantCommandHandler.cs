using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Queries.GetDishFromRestaurant;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDish;

public class DeleteDishFromRestaurantCommandHandler(ILogger<GetDishFromRestaurantQueryHandler> logger, IRestaurantRepository repository,
    IDishesRepository dishRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService)
    : IRequestHandler<DeleteDishFromRestaurantCommand>
{
    public async Task Handle(DeleteDishFromRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving dishes from {Restaurant} with Id : {Id}", typeof(Restaurant), request.RestaurantId);
        var restaurant = await repository.GetById(request.RestaurantId);

        if (restaurant is null)
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }

        if (!restaurantAuthorizationService.Authorize(restaurant, RessourceOperation.Update))
            throw new ForbidException();

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);

        if (dish is null)
        {
            throw new NotFoundException(nameof(Dish), request.DishId.ToString());
        }

        await dishRepository.Delete(dish);
    }
}
