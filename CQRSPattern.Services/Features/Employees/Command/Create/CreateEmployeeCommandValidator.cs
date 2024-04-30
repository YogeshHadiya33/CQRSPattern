using FluentValidation;

namespace CQRSPattern.Services.Features.Employees.Command.Create;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.Employee)
            .NotNull()
            .WithMessage("Employee details are required");

        When(x => x.Employee != null, () =>
        {
            RuleFor(x => x.Employee.FirstName)
                .NotEmpty()
                .WithMessage("First Name is required");

            RuleFor(x => x.Employee.LastName)
                .NotEmpty()
                .WithMessage("Last Name is required");

            RuleFor(x => x.Employee.Phone)
                .NotEmpty()
                .WithMessage("Phone is required");

            RuleFor(x => x.Employee.Department)
                .NotEmpty()
                .WithMessage("Department is required");

            RuleFor(x => x.Employee.Designation)
                .NotEmpty()
                .WithMessage("Designation is required");

            RuleFor(x => x.Employee.Address)
                .NotEmpty()
                .WithMessage("Address is required");

            RuleFor(x => x.Employee.Email)
                .NotEmpty()
                .WithMessage("Email is required");

            When(x => !string.IsNullOrEmpty(x.Employee.Email), () =>
            {
                RuleFor(x => x.Employee.Email)
                    .EmailAddress()
                    .WithMessage("Email should be valid");
            });
        });
    }
}