using Microsoft.Extensions.DependencyInjection;
using MediatR;
using MediatRDemo.Behaviors;

namespace MediatRDemo;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediatRModuleDI(this IServiceCollection services)
    {
        // Add MediatR services
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(FakeDataStore).Assembly);
            cfg.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));
        });
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingPipelineBehavior<,>));

        services.AddSingleton<FakeDataStore>();

        return services;
    }
}
