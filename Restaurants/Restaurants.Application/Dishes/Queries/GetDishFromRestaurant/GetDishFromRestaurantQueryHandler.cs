using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishFromRestaurant;

public class GetDishFromRestaurantQueryHandler(ILogger<GetDishFromRestaurantQueryHandler> logger, IRestaurantRepository repository) 
    : IRequestHandler<GetDishFromRestaurantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishFromRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving dishes from {Restaurant} with Id : {Id}", typeof(Restaurant), request.RestaurantId);
        var restaurant = await repository.GetById(request.RestaurantId);

        if(restaurant is null)
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }

        var dishDto = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);

        if (dishDto is null)
        {
            throw new NotFoundException(nameof(Dish), request.DishId.ToString());
        }

        return DishDto.FromEntity(dishDto);
    }
}
