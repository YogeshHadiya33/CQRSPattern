using MediatR;

namespace CQRSPattern.Common.CommandBus;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}