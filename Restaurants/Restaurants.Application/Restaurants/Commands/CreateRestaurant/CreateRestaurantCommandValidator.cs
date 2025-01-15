using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class UpdateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validCategories = ["Fast-Food", "Mexicain", "Japonais"];
    public UpdateRestaurantCommandValidator()
    {
        RuleFor(r => r.Name)
            .Length(5, 50);

        RuleFor(r => r.Description)
            .NotEmpty()
            .WithMessage("La description est obligatoire");

        RuleFor(r => r.Category)
            .NotEmpty()
            .WithMessage("La category est obligatoire !");

        RuleFor(r => r.ContactEmail)
            .EmailAddress()
            .WithMessage("Veuillez entrer un mail valide");

        RuleFor(r => r.PostalCode)
            .Matches(@"\d{5}")
            .WithMessage("Veuillez entrer un code postal sous la forme XXXXX");

        RuleFor(r => r.Category)
            .Must(category => validCategories.Contains(category!))
            .WithMessage("La catégories choisie est invalide !");
        //.Custom((value, context) =>
        //{
        //    var isValidCategory = validCategories.Contains(value);
        //    if (!isValidCategory)
        //    {
        //        context.AddFailure("Category", "La catégories choisie est invalide !");
        //    }
        //});

    }

}
