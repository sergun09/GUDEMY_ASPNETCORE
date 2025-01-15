using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class CreateDishCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(r => r.Name)
            .Length(5, 50);
    }
}
