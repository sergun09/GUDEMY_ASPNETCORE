using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IDishesRepository
{
    Task<IEnumerable<Dish>> GetAllByRestaurantAsync(int restaurantId);

    Task<Dish?> GetById(int id);

    Task<int> Create(Dish dish);
    Task Delete(Dish dish);
    Task Update(Dish dish);
}
