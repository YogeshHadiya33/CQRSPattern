using CQRSPattern.Common.CommandBus;
using CQRSPattern.Entity.Employee.Bussiness;

namespace CQRSPattern.Services.Features.Employees.Command.Update;

public class UpdateEmployeeCommand : ICommand<ICommandResult>
{
    public string UserId { get; set; }
    public UpdateEmployeeModel Employee { get; set; }
}