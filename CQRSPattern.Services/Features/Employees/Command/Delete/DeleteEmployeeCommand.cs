using CQRSPattern.Common.CommandBus;

namespace CQRSPattern.Services.Features.Employees.Command.Delete;

public class DeleteEmployeeCommand : ICommand<ICommandResult>
{
    public int Id { get; set; }
}