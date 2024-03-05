using CQRSPattern.Common.CommandBus;
using CQRSPattern.Common.Common;
using CQRSPattern.Common.Factory;
using CQRSPattern.Services.Features.Employees.Repositories;
using Microsoft.AspNetCore.Http;

namespace CQRSPattern.Services.Features.Employees.Command.Delete;

public class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand, ICommandResult>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICommandResultFactory _commandResultFactory;

    public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, ICommandResultFactory commandResultFactory)
    {
        _employeeRepository = employeeRepository;
        _commandResultFactory = commandResultFactory;
    }

    public async Task<ICommandResult> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.Get(request.Id);

        if (employee is null)
            return _commandResultFactory.Create(false, StatusCodes.Status404NotFound, Constants.NotFound);

        var deleted = await _employeeRepository.Delete(request.Id);
        if (!deleted)
            return _commandResultFactory.Create(false, StatusCodes.Status500InternalServerError);

        return _commandResultFactory.Create(true, StatusCodes.Status200OK, Constants.Delete);
    }
}
