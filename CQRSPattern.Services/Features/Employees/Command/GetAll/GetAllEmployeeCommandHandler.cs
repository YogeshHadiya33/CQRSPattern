using CQRSPattern.Common.Builder.Mapper;
using CQRSPattern.Common.Caching.Service;
using CQRSPattern.Common.CommandBus;
using CQRSPattern.Common.Common;
using CQRSPattern.Common.ExtentionMethods;
using CQRSPattern.Common.Factory;
using CQRSPattern.Entity.Employee.Bussiness;
using CQRSPattern.Services.Features.Employees.Repositories;
using Microsoft.AspNetCore.Http;

namespace CQRSPattern.Services.Features.Employees.Command.GetAll;

public class GetAllEmployeeCommandHandler : ICommandHandler<GetAllEmployeeCommand, ICommandResult>
{
    private readonly IEmployeeRepository _employeeService;
    private readonly ICommandResultFactory _commandResultFactory;
    private readonly ICustomMapper<List<EmployeeModel>> _customMapper;
    private readonly IInMemoryRedisCacheService<List<EmployeeModel>> _cacheService;

    public GetAllEmployeeCommandHandler(ICommandResultFactory commandResultFactory,
        IEmployeeRepository employeeService,
        ICustomMapper<List<EmployeeModel>> customMapper,
        IInMemoryRedisCacheService<List<EmployeeModel>> cacheService)
    {
        _commandResultFactory = commandResultFactory;
        _employeeService = employeeService;
        _customMapper = customMapper;
        _cacheService = cacheService;
    }

    public async Task<ICommandResult> Handle(GetAllEmployeeCommand request, CancellationToken cancellationToken)
    {
        const string cacheKey = $"{Constants.CacheKeys.Employee}All";

        var response = _cacheService.Get(cacheKey);

        if (response != null)
            return _commandResultFactory.Create(true, StatusCodes.Status200OK, response);

        var employees = await _employeeService.GetAll();

        if (!employees.HasValue())
            return _commandResultFactory.Create(false, Constants.NotFound, StatusCodes.Status404NotFound);

        response = _customMapper.AddSource(employees).Map();

        _cacheService.Set(cacheKey, response);

        return _commandResultFactory.Create(true, StatusCodes.Status200OK, response);
    }
}