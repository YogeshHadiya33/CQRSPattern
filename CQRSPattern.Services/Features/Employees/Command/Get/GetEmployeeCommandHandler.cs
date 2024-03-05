using CQRSPattern.Common.Builder.Mapper;
using CQRSPattern.Common.CommandBus;
using CQRSPattern.Common.Common;
using CQRSPattern.Common.Factory;
using CQRSPattern.Entity.Employee.Bussiness;
using CQRSPattern.Services.Features.Employees.Repositories;
using Microsoft.AspNetCore.Http;

namespace CQRSPattern.Services.Features.Employees.Command.Get;

public class GetEmployeeCommandHandler : ICommandHandler<GetEmployeeCommand, ICommandResult>
{
    private readonly IEmployeeRepository _employeeService;
    private readonly ICommandResultFactory _commandResultFactory;
    private readonly ICustomMapper<EmployeeModel> _customMapper;

    public GetEmployeeCommandHandler(IEmployeeRepository employeeService, ICommandResultFactory commandResultFactory, ICustomMapper<EmployeeModel> customMapper)
    {
        _employeeService = employeeService;
        _commandResultFactory = commandResultFactory;
        _customMapper = customMapper;
    }

    public async Task<ICommandResult> Handle(GetEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            return _commandResultFactory.Create(false, StatusCodes.Status400BadRequest);

        var employee = await _employeeService.Get(request.Id);

        if (employee is null)
            return _commandResultFactory.Create(false, Constants.NotFound, StatusCodes.Status404NotFound);

        var response = _customMapper.AddSource(employee).Map();

        return _commandResultFactory.Create(true, StatusCodes.Status200OK, response);
    }
}
