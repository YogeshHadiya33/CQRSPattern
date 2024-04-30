using CQRSPattern.Common.Builder.Mapper;
using CQRSPattern.Common.Caching.Service;
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
    private readonly IInMemoryRedisCacheService<EmployeeModel> _cacheService;

    public GetEmployeeCommandHandler(IEmployeeRepository employeeService,
        ICommandResultFactory commandResultFactory,
        ICustomMapper<EmployeeModel> customMapper,
        IInMemoryRedisCacheService<EmployeeModel> cacheService)
    {
        _employeeService = employeeService;
        _commandResultFactory = commandResultFactory;
        _customMapper = customMapper;
        _cacheService = cacheService;
    }

    public async Task<ICommandResult> Handle(GetEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            return _commandResultFactory.Create(false, StatusCodes.Status400BadRequest);

        var cacheKey = $"{Constants.CacheKeys.Employee}{request.Id}";

        var response = _cacheService.Get(cacheKey);

        if (response == null)
        {
            var employee = await _employeeService.Get(request.Id);

            if (employee is null)
                return _commandResultFactory.Create(false, Constants.NotFound, StatusCodes.Status404NotFound);

            response = _customMapper.AddSource(employee).Map();

            _cacheService.Set(cacheKey, response);
        }

        return _commandResultFactory.Create(true, StatusCodes.Status200OK, response);
    }
}