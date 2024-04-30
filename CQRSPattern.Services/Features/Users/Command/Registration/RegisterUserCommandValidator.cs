using FluentValidation;

namespace CQRSPattern.Services.Features.Users.Command.Registration;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Registration.Email)
            .NotEmpty()
            .WithMessage("Email is required");

        RuleFor(x => x.Registration.Password)
            .NotEmpty()
            .WithMessage("Password is required");

        RuleFor(x => x.Registration.FirstName)
            .NotEmpty()
            .WithMessage("First Name is required");

        RuleFor(x => x.Registration.LastName)
            .NotEmpty()
            .WithMessage("Last Name is required");

        RuleFor(x => x.Registration.DateOfBirth)
            .NotEmpty()
            .WithMessage("Date Of Birth is required");

        When(x => Convert.ToDateTime(x.Registration.DateOfBirth) != DateTime.MinValue, () =>
        {
            RuleFor(x => x.Registration.DateOfBirth)
                .Must(x => Convert.ToDateTime(x).AddYears(10) < DateTime.Now)
                .WithMessage("Date of birth should be before 10 years");
        });
    }
}