using CQRSPattern.Common.CommandBus;

namespace CQRSPattern.Services.Features.Employees.Command.Create;

public class CreateEmployeeCommandEventHandler : IEventHandler<CreateEmployeeCommand, ICommandResult>
{
    public async Task Process(CreateEmployeeCommand request, ICommandResult response, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}