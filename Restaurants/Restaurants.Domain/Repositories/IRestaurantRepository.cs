using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();

    Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchTerm);

    Task<Restaurant?> GetById(int id);

    Task<int> Create(Restaurant restaurant);
    Task Delete(Restaurant restaurant);
    Task Update(Restaurant restaurant);
}
