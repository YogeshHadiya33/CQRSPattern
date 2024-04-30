using MediatR;

namespace CQRSPattern.Common.CommandBus;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : IRequest<TResponse>
{
}