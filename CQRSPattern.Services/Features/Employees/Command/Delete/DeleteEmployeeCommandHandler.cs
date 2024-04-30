using CQRSPattern.Common.Caching.Service;
using CQRSPattern.Common.CommandBus;
using CQRSPattern.Common.Common;
using CQRSPattern.Common.Factory;
using CQRSPattern.Entity.Employee.Bussiness;
using CQRSPattern.Services.Features.Employees.Repositories;
using Microsoft.AspNetCore.Http;

namespace CQRSPattern.Services.Features.Employees.Command.Delete;

public class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand, ICommandResult>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICommandResultFactory _commandResultFactory;
    private readonly IInMemoryRedisCacheService<EmployeeModel> _cacheService;

    public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository,
        ICommandResultFactory commandResultFactory,
        IInMemoryRedisCacheService<EmployeeModel> cacheService)
    {
        _employeeRepository = employeeRepository;
        _commandResultFactory = commandResultFactory;
        _cacheService = cacheService;
    }

    public async Task<ICommandResult> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.Get(request.Id);

        if (employee is null)
            return _commandResultFactory.Create(false, StatusCodes.Status404NotFound, Constants.NotFound);

        var deleted = await _employeeRepository.Delete(request.Id);
        if (!deleted)
            return _commandResultFactory.Create(false, StatusCodes.Status500InternalServerError);

        _cacheService.Remove($"{Constants.CacheKeys.Employee}All");
        _cacheService.Remove($"{Constants.CacheKeys.Employee}{request.Id}");

        return _commandResultFactory.Create(true, StatusCodes.Status200OK, Constants.Delete);
    }
}