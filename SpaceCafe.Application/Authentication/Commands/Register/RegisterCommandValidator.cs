using FluentValidation;

namespace SpaceCafe.Application.Authentication.Commands.Register;
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name area cannot be empty");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name area cannot be empty");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email area cannot be empty");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(3).WithMessage("Password must be atleast 3 character and cannot be empty");
    }
}
