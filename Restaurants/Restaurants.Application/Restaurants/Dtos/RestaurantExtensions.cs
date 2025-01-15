using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

internal static class RestaurantExtensions
{
    public static Restaurant ToEntiy(this CreateRestaurantCommand dto)
    {
        return new Restaurant
        {
            Name = dto.Name,
            Description = dto.Description,
            ContactEmail = dto.ContactEmail,
            ContactNumber = dto.ContactNumber,
            Category = dto.Category,
            HasDelivery = dto.HasDelivery,
            Address = new Address
            {
                City = dto.City,
                PostalCode = dto.PostalCode,
                Street = dto.Street,
            }
        };
    }
}
