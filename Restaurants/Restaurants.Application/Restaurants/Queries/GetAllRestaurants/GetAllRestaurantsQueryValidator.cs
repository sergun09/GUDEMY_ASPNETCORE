using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private readonly int[] allowedPageSizes = [5, 10, 15, 20];
    private readonly string[] allowedSortBy = [nameof(RestaurantDto.Name), nameof(RestaurantDto.Description), nameof(RestaurantDto.Category)];

    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => allowedPageSizes.Contains(value))
            .WithMessage($"Page Sizes must be in [{string.Join(",", allowedPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => allowedSortBy.Contains(value))
            .When(value => value.SortBy is not null)
            .WithMessage($"Sort must be empty or equals to [{string.Join(",", allowedSortBy)}]");
    }
}
