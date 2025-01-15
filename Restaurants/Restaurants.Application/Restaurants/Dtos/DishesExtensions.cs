using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public static class DishesExtensions
{
    public static Dish ToEntity(this CreateDishCommand command)
    {
        return new Dish
        {
            Description = command.Description,
            Name = command.Name,
            KiloCalories = command.KiloCalories,
            RestaurantId = command.RestaurantId,
            Price = command.Price,
        };
    }
}
