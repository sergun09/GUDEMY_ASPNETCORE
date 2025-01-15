using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class DishesRepository : IDishesRepository
{
    private readonly RestaurantDbContext _context;

    public DishesRepository(RestaurantDbContext context)
    {
        this._context = context;
    }

    public async Task<int> Create(Dish dish)
    {
        _context.Dishes.Add(dish);
        await _context.SaveChangesAsync();
        return dish.Id;
    }

    public async Task Delete(Dish dish)
    {
        _context.Dishes.Remove(dish);
        await (_context.SaveChangesAsync());
    }

    public async Task<IEnumerable<Dish>> GetAllByRestaurantAsync(int restaurantId)
    {
        return await _context.Dishes.Where(d => d.RestaurantId == restaurantId).ToListAsync();
    }

    public Task<Dish?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Dish dish)
    {
        _context.Dishes.Update(dish);
        await _context.SaveChangesAsync();
    }
}
