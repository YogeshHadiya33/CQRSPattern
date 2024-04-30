using CQRSPattern.Common.CommandBus;
using CQRSPattern.Entity.Employee.Bussiness;

namespace CQRSPattern.Services.Features.Employees.Command.Create;

public class CreateEmployeeCommand : ICommand<ICommandResult>
{
    public string UserId { get; set; }
    public CreateEmployeeModel Employee { get; set; }
}