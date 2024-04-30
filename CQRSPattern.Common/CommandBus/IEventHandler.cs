using MediatR;
using MediatR.Pipeline;

namespace CQRSPattern.Common.CommandBus;

public interface IEventHandler<in TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
}