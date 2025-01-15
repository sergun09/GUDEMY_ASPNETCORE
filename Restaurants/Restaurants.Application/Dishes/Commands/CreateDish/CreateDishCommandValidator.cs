using FluentValidation;
using Restaurants.Application.Dishes.Commands.CreateDish;

namespace Restaurants.Application.Dishes.Commands;

public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(d => d.Name)
            .Length(5, 50);

        RuleFor(d => d.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Le prix doit être positif");

        RuleFor(d => d.KiloCalories)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Les Kcals doivent être positifs");
    }
}
