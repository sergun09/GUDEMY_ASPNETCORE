using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, IRestaurantRepository repository, IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("{Username} with {UserId] Creating restaurant {@Restaurant}", user.Email, user.Id, request);

        var restaurant = request.ToEntiy();
        restaurant.OwnerId = user.Id;

        int id = await repository.Create(restaurant);
        return id;
    }
}
