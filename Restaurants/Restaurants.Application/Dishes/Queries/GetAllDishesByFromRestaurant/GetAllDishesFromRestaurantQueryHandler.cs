using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetAllDishesByFromRestaurant;

public class GetAllDishesFromRestaurantQueryHandler(ILogger<GetAllDishesFromRestaurantQueryHandler> logger, IDishesRepository repository) 
    : IRequestHandler<GetAllDishesFromRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetAllDishesFromRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving dishes from {Restaurant} with Id : {Id}", typeof(Restaurant), request.Id);
        var dishesFromRestaurant = await repository.GetAllByRestaurantAsync(request.Id);

        var dishesDto = dishesFromRestaurant.Select(DishDto.FromEntity).ToList();
        return dishesDto;
    }
}
