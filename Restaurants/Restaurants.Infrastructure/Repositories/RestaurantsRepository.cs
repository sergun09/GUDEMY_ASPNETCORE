using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository : IRestaurantRepository
{
    private readonly RestaurantDbContext _context;

    public RestaurantsRepository(RestaurantDbContext context)
    {
        this._context = context;
    }

    public async Task<int> Create(Restaurant restaurant)
    {
        _context.Restaurants.Add(restaurant);
        await _context.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task Delete(Restaurant restaurant)
    {
        _context.Restaurants.Remove(restaurant);
        await (_context.SaveChangesAsync());
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return await _context.Restaurants
            .Include(r => r.Dishes)
            .ToListAsync();
    }

    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchTerm, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
    {
        var searchPhrase = searchTerm?.ToLower();

        var baseQuery = _context.Restaurants
            .Where(r => searchPhrase == null || r.Name.ToLower().Contains(searchPhrase) || r.Description.ToLower().Contains(searchPhrase));

        var totalCount = await baseQuery.CountAsync();

        if(sortBy is not null)
        {
            var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                { nameof(Restaurant.Name), x => x.Name },
                { nameof(Restaurant.Description), x => x.Description},
                { nameof(Restaurant.Category), x => x.Category },
            };

            var selectedColumn = columnSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Ascending ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn); 
        }

        var restaurants = await baseQuery
            .Include(r => r.Dishes)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (restaurants, totalCount);
    }

    public async Task<Restaurant?> GetById(int id)
    {
        return await _context.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task Update(Restaurant restaurant)
    {
        _context.Restaurants.Update(restaurant);
        await _context.SaveChangesAsync();
    }
}
