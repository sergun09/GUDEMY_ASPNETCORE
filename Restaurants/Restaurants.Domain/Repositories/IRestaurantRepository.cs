using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetById(int id);

    Task<int> Create(Restaurant restaurant);
    Task Delete(Restaurant restaurant);
    Task Update(Restaurant restaurant);
    Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchTerm, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
}
