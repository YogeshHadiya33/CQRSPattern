using FluentValidation;
using FluentValidation.Results;

namespace CQRSPattern.Common.Exception;

public class CustomValidationException : ValidationException
{
    public CustomValidationException(IEnumerable<ValidationFailure> failures) : base(failures)
    {
    }

    public override string Message
    {
        get
        {
            var errorMessages = Errors
                .Select(failure => $"-- {failure.PropertyName}: {failure.ErrorMessage}")
                .ToList();

            return $"Validation failed:\n{string.Join("\n", errorMessages)}";
        }
    }
}