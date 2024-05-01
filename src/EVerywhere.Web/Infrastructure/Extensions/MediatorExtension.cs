using EVerywhere.Balance.Application.Interfaces;
using EVerywhere.Web.Behaviors;
using MediatR;
using MediatR.Pipeline;

namespace EVerywhere.Web.Infrastructure.Extensions;

public static class MediatorExtension
{
    public static IServiceCollection AddCustomMediator(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssemblies([typeof(IBalanceDbContext).Assembly]);
            cfg.AddRequestPreProcessor(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });

        return serviceCollection;
    }
}