using FluentValidation;

namespace CQRSPattern.Services.Features.Users.Command.SignIn;

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(x => x.SignInModel.Username)
            .NotEmpty()
            .WithMessage("User name is required");

        RuleFor(x => x.SignInModel.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}