using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("RestaurantsDb"))
                .EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
            // Ajout des roles dans le token JWT
            .AddRoles<IdentityRole>()
            // Ajout de custom claims dans le token JW
            .AddClaimsPrincipalFactory<UserClaimPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantDbContext>();

        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();

        services.AddScoped<IRestaurantRepository, RestaurantsRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();


        services.AddScoped<IAuthorizationHandler, MinAgeRequirementHandler>();
        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality, "French", "Turkish"))
            .AddPolicy(PolicyNames.AtLeast20, builder =>
            {
                builder.AddRequirements(new MinAgeRequirement(18));
            });

    }

}
