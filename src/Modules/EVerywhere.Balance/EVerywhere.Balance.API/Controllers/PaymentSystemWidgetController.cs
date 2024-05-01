using EVerywhere.Balance.Application.Features.PaymentSystemWidgetFeatures.Queries;
using EVerywhere.ModulesCommon.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EVerywhere.Balance.API.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class PaymentSystemWidgetController(ISender sender, IMediator mediator)
    : MediatrController(sender, mediator)
{
    /// <summary>
    /// Получить активный виджет пользователя
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("getActive")]
    public async Task<CheckoutDto> GetActiveWidget(CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetActivePaymentSystemWidgetQuery(), cancellationToken);
    }
}