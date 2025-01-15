﻿using Restaurants.Application.Dishes;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public bool HasDelivery { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public List<DishDto> Dishes { get; set; }

    public static RestaurantDto? FromEntity(Restaurant? r)
    {
        if (r == null) return null;
        
        return new RestaurantDto
        {
            Category = r.Category,
            Description = r.Description,
            Id = r.Id,
            HasDelivery = r.HasDelivery,
            Name = r.Name,
            City = r.Address?.City,
            PostalCode = r.Address?.PostalCode,
            Street = r.Address?.Street,
            Dishes = r.Dishes.Select(d => DishDto.FromEntity(d)).ToList()
        };
    }
}
