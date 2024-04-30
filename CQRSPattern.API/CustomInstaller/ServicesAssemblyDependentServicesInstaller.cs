using CQRSPattern.Behaviours;
using CQRSPattern.Common.CommandBus;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;

namespace CQRSPattern.API.CustomInstaller;

public static class ServicesAssemblyDependentServicesInstaller
{
    private const string ServicesAssemblyName = "CQRSPattern.Service";

    public static void InstallAssemblyDependentServices(this IServiceCollection services)
    {
        var servicesAssembly = AppDomain.CurrentDomain
            .GetAssemblies()
            .FirstOrDefault(a => a.FullName != null && a.FullName.StartsWith(ServicesAssemblyName, StringComparison.InvariantCulture));

        if (servicesAssembly is null)
            return;

        // MediatR
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblies(servicesAssembly);

            var eventHandlerTypes = servicesAssembly.GetTypes()
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<,>)));

            foreach (var handlerType in eventHandlerTypes)
            {
                var implementedInterfaces = handlerType.GetInterfaces().Where(x => x.GetGenericTypeDefinition() == typeof(IEventHandler<,>));

                foreach (var implementedInterface in implementedInterfaces)
                {
                    var genericArguments = implementedInterface.GetGenericArguments();

                    if (genericArguments.Length >= 2)
                    {
                        var requestType = genericArguments[0];
                        var responseType = genericArguments[1];

                        var postProcessorType = typeof(IRequestPostProcessor<,>).MakeGenericType(requestType, responseType);

                        x.AddRequestPostProcessor(postProcessorType, handlerType);
                    }
                }
            }
        });

        //Auto Mapper
        services.AddAutoMapper(servicesAssembly);

        //Validation
        services.AddValidatorsFromAssembly(servicesAssembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}