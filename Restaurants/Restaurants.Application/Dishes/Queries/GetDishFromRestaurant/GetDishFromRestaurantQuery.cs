using MediatR;

namespace Restaurants.Application.Dishes.Queries.GetDishFromRestaurant;

public class GetDishFromRestaurantQuery : IRequest<DishDto>
{
    public GetDishFromRestaurantQuery(int restaurantId, int dishId)
    {
        RestaurantId = restaurantId;
        DishId = dishId;
    }

    public int RestaurantId { get; set; }
    public int DishId { get; set; }

}
