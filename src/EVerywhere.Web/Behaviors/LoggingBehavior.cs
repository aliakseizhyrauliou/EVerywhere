using EVerywhere.ModulesCommon.Application.Interfaces;
using MediatR.Pipeline;

namespace EVerywhere.Web.Behaviors;

public class LoggingBehaviour<TRequest>(ILogger<TRequest> logger, IUser user)
    : IRequestPreProcessor<TRequest>
{
    private readonly ILogger _logger = logger;

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = user.Id ?? string.Empty;
        
        _logger.LogWarning("Barion.Balance Request: {@RequestName} {@UserId} {@Request}",
            requestName, userId,  request);
    }
}