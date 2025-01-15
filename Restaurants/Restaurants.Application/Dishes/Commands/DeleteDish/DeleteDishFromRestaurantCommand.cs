using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDish;

public class DeleteDishFromRestaurantCommand : IRequest
{
    public DeleteDishFromRestaurantCommand(int restaurantId, int dishId)
    {
        RestaurantId = restaurantId;
        DishId = dishId;
    }

    public int RestaurantId { get; set; }
    public int DishId { get; set; }
}
