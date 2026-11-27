namespace lr1.Validators;
using FluentValidation;
using lr1.Models;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ім’я не має бути пустим");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email не має бути пустим")
            .EmailAddress().WithMessage("Невірний формат Email");
    }
}