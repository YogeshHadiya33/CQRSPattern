using CQRSPattern.Common.Builder.Mapper;
using CQRSPattern.Common.CommandBus;
using CQRSPattern.Common.Common;
using CQRSPattern.Common.Factory;
using CQRSPattern.Entity.Employee.Bussiness;
using CQRSPattern.Entity.Employee.Database;
using CQRSPattern.Services.Features.Employees.Builder;
using CQRSPattern.Services.Features.Employees.Repositories;
using Microsoft.AspNetCore.Http;

namespace CQRSPattern.Services.Features.Employees.Command.Update;

public class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand, ICommandResult>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICommandResultFactory _commandResultFactory;
    private readonly IEmployeeBuilder _employeeBuilder;
    private readonly ICustomMapper<EmployeeModel> _customMapper;

    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, ICommandResultFactory commandResultFactory, IEmployeeBuilder employeeBuilder, ICustomMapper<EmployeeModel> customMapper)
    {
        _employeeRepository = employeeRepository;
        _commandResultFactory = commandResultFactory;
        _employeeBuilder = employeeBuilder;
        _customMapper = customMapper;
    }

    public async Task<ICommandResult> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.Get(request.Employee.Id);
        if (employee is null)
            return _commandResultFactory.Create(false, StatusCodes.Status404NotFound, Constants.NotFound);

        if (await IsOtherEmployeeExist(request.Employee))
            return _commandResultFactory.Create(false, StatusCodes.Status409Conflict, Constants.AlreadyExist);

        var updatedRecord = UpdateEmployee(request.Employee, employee, request.UserId);

        _ = await _employeeRepository.Update(updatedRecord);

        return _commandResultFactory.Create(true, StatusCodes.Status201Created, _customMapper.AddSource(updatedRecord).Map());
    }

    private Employee UpdateEmployee(UpdateEmployeeModel employeeModel, Employee oldEmployee, string userId)
    {
        return _employeeBuilder
            .AddExistingEmployee(oldEmployee)
            .AddEmail(employeeModel.Email)
            .AddFirstName(employeeModel.FirstName)
            .AddLastName(employeeModel.LastName)
            .AddPhoneNumber(employeeModel.Phone)
            .AddEmail(employeeModel.Email)
            .AddDepartment(employeeModel.Department)
            .AddDesignation(employeeModel.Designation)
            .AddAddress(employeeModel.Address)
            .AddUpdatedDetails(userId)
            .Build();
    }

    private async Task<bool> IsOtherEmployeeExist(UpdateEmployeeModel employee)
    {
        var data = await _employeeRepository.Get(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName && x.Id != employee.Id);
        return data != null;
    }
}
