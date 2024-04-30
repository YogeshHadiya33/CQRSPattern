using CQRSPattern.Common.CommandBus;

namespace CQRSPattern.Services.Features.Employees.Command.Get;

public class GetEmployeeCommand : ICommand<ICommandResult>
{
    public int Id { get; set; }
}