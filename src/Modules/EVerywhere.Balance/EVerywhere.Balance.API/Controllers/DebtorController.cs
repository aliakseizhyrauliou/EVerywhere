using EVerywhere.Balance.Application.Features.DebtorFeatures.Commands;
using EVerywhere.ModulesCommon.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EVerywhere.Balance.API.Controllers;

public class DebtorController(ISender sender, IMediator mediator) : MediatrController(sender, mediator)
{
    /// <summary>
    /// Удержать сумму с должника
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost("captureDebtor")]
    public async Task CaptureDebtor([FromBody] CaptureDebtorCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
    }
}