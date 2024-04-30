using CQRSPattern.Common.Builder.Mapper;
using CQRSPattern.Common.Caching.Service;
using CQRSPattern.Common.CommandBus;
using CQRSPattern.Common.Common;
using CQRSPattern.Common.Factory;
using CQRSPattern.Entity.Employee.Bussiness;
using CQRSPattern.Entity.Employee.Database;
using CQRSPattern.Services.Features.Employees.Builder;
using CQRSPattern.Services.Features.Employees.Repositories;
using Microsoft.AspNetCore.Http;

namespace CQRSPattern.Services.Features.Employees.Command.Create;

public class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, ICommandResult>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICommandResultFactory _commandResultFactory;
    private readonly IEmployeeBuilder _employeeBuilder;
    private readonly ICustomMapper<EmployeeModel> _customMapper;
    private readonly IInMemoryRedisCacheService<List<EmployeeModel>> _cacheService;

    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository,
        ICommandResultFactory commandResultFactory,
        IEmployeeBuilder employeeBuilder,
        ICustomMapper<EmployeeModel> customMapper,
        IInMemoryRedisCacheService<List<EmployeeModel>> cacheService)
    {
        _employeeRepository = employeeRepository;
        _commandResultFactory = commandResultFactory;
        _employeeBuilder = employeeBuilder;
        _customMapper = customMapper;
        _cacheService = cacheService;
    }

    public async Task<ICommandResult> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (await IsEmployeeExist(request.Employee))
            return _commandResultFactory.Create(false, StatusCodes.Status409Conflict, Constants.AlreadyExist);

        var createdEmployee = await _employeeRepository.Create(CreateEmployeeObject(request.Employee, request.UserId));
        if (createdEmployee is null)
            return _commandResultFactory.Create(false, StatusCodes.Status500InternalServerError);

        _cacheService.Remove($"{Constants.CacheKeys.Employee}All");

        return _commandResultFactory.Create(true, StatusCodes.Status201Created, _customMapper.AddSource(createdEmployee).Map());
    }

    private Employee CreateEmployeeObject(CreateEmployeeModel employeeModel, string userId)
    {
        return _employeeBuilder.AddEmail(employeeModel.Email)
            .AddFirstName(employeeModel.FirstName)
            .AddLastName(employeeModel.LastName)
            .AddPhoneNumber(employeeModel.Phone)
            .AddEmail(employeeModel.Email)
            .AddDepartment(employeeModel.Department)
            .AddDesignation(employeeModel.Designation)
            .AddAddress(employeeModel.Address)
            .AddCreatedDetails(userId)
            .Build();
    }

    private async Task<bool> IsEmployeeExist(CreateEmployeeModel employee)
    {
        var data = await _employeeRepository.Get(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName);
        return data != null;
    }
}