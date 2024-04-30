using CQRSPattern.Common.CommandBus;
using CQRSPattern.Common.Exception;
using CQRSPattern.Common.Factory;
using FluentValidation;
using MediatR;

namespace CQRSPattern.Behaviours;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ICommandResultFactory _commandResultFactory;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ICommandResultFactory commandResultFactory)
    {
        _validators = validators;
        _commandResultFactory = commandResultFactory;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Any())
        {
            if (typeof(TResponse) == typeof(ICommandResult))
            {
                var response = _commandResultFactory.Create(false, StatusCodes.Status400BadRequest, failures.Select(x => $"{x.ErrorMessage}").ToList());
                return (TResponse)(ICommandResult)response;
            }
            else
                throw new CustomValidationException(failures);
        }

        return await next();
    }
}