using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder : IRestaurantSeeder
{
    private readonly RestaurantDbContext _context;

    public RestaurantSeeder(RestaurantDbContext context)
    {
        this._context = context;
    }

    public async Task Seed()
    {
        if(await _context.Database.CanConnectAsync())
        {
            if (!_context.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                _context.Restaurants.AddRange(restaurants);
                await _context.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        return new List<Restaurant>()
        {
            new Restaurant 
            {
                Name = "KFC",
                Category = "Fast-Food",
                Description = "Restaurant spécialisé dans le poulet !",
                ContactEmail = "contact@kfc.com",
                ContactNumber = "0000000000",
                HasDelivery = true,

                Dishes = new List<Dish>()
                {
                    new Dish()
                    {
                        Name = "KFC Tenders",
                        Description = "Des morceaux de poulet tendres !",
                        Price = 10.50m
                    },
                    new Dish()
                    {
                        Name = "KFC Wings",
                        Description = "Des morceaux bien épicés !",
                        Price = 8.50m,
                    },
                },

                Address = new()
                {
                    City = "Chartres",
                    PostalCode = "28000",
                    Street = "Rue des Moulins"
                }
            },
            new Restaurant
            {
                Name = "Quick",
                Category = "Fast-Food",
                Description = "Restaurant spécialisé dans le burger !",
                ContactEmail = "contact@quick.com",
                ContactNumber = "0000000000",
                HasDelivery = true,

                Dishes = new List<Dish>()
                {
                    new Dish()
                    {
                        Name = "Double Cheese",
                        Description = "Un burger double !",
                        Price = 12.50m
                    },
                    new Dish()
                    {
                        Name = "Triple Cheese",
                        Description = "Un burger triple !",
                        Price = 6.50m
                    },
                },

                Address = new()
                {
                    City = "Paris",
                    PostalCode = "75001",
                    Street = "Rue des Boucheries"
                }
            }
        };
    }
}
