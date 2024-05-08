using System.Reflection;
using EVerywhere.Web.Behaviors;
using MediatR;
using MediatR.Pipeline;

namespace EVerywhere.Web.Infrastructure.Extensions;

public static class MediatorExtension
{
    public static IServiceCollection AddCustomMediator(this IServiceCollection serviceCollection, Assembly[] assemblies)
    {
        serviceCollection.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssemblies(assemblies);
            cfg.AddRequestPreProcessor(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });

        return serviceCollection;
    }
}