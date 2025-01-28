using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IRestaurantRepository repository)
    : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
{
    public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all restaurants");
        var (restaurants, totalCount) = await repository.GetAllMatchingAsync(
            request.SearchTerm, 
            request.PageSize, 
            request.PageNumber,
            request.SortBy,
            request.SortDirection);

        var restaurantsDtos = restaurants.Select(r => RestaurantDto.FromEntity(r));

        var result = new PagedResult<RestaurantDto>(restaurantsDtos,totalCount, request.PageSize, request.PageNumber);

        return result;
    }
}
