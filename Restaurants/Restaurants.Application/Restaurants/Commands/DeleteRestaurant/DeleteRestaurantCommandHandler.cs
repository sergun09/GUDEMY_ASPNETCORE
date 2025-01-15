using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantRepository repository) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Restaurant with Id {RestaurantId} with {@Restaurant}", request.Id, request);
        var restaurant = await repository.GetById(request.Id);

        if (restaurant is null) 
        {
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        }

        await repository.Delete(restaurant); 

        return true;
    }
}
