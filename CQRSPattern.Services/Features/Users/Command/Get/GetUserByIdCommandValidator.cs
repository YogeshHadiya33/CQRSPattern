using FluentValidation;

namespace CQRSPattern.Services.Features.Users.Command.Get;

public class GetUserByIdCommandValidator : AbstractValidator<GetUserByIdCommand>
{
    public GetUserByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User Id is required");
    }
}