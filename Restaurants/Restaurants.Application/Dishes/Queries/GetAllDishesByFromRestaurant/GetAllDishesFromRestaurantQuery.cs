using MediatR;

namespace Restaurants.Application.Dishes.Queries.GetAllDishesByFromRestaurant;

public class GetAllDishesFromRestaurantQuery : IRequest<IEnumerable<DishDto>>
{
    public int Id { get; }

    public GetAllDishesFromRestaurantQuery(int id)
    {
        Id = id;
    }
}
