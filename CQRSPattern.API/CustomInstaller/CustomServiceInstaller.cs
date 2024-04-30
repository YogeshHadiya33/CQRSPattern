using CQRSPattern.Common.AppSettings.Genric;
using CQRSPattern.Common.Builder.Mapper;
using CQRSPattern.Common.Caching.Service;
using CQRSPattern.Common.CommandBus;
using CQRSPattern.Common.Factory;
using CQRSPattern.Services.Features.Employees.Builder;
using CQRSPattern.Services.Features.Employees.Repositories;
using CQRSPattern.Services.Features.Users.Services;

namespace CQRSPattern.API.CustomInstaller;

public static class CustomServiceInstaller
{
    public static void InstallCustomService(this IServiceCollection services)
    {
        services.AddTransient<ICommandResultFactory, CommandResultFactory>();
        services.AddTransient<ICommandResult, CommandResult>();
        services.AddTransient<IEmployeeBuilder, EmployeeBuilder>();
        services.AddTransient(typeof(ICustomMapper<>), typeof(CustomMapper<>));
        services.AddTransient(typeof(IGenericAppSettings<>), typeof(GenericAppSettings<>));

        services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        services.AddTransient<IIdentityTokenService, IdentityTokenService>();

        services.AddTransient(typeof(IInMemoryRedisCacheService<>), typeof(InMemoryRedisCacheService<>));
        services.AddTransient(typeof(IRedisCacheService<>), typeof(RedisRedisCacheService<>));
    }
}