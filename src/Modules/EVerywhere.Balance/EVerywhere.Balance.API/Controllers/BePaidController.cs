using Balance.BePaid.Web.Infrastructure.Attributes;
using EVerywhere.Balance.API.Attributes;
using EVerywhere.Balance.API.Helpers;
using EVerywhere.Balance.Application.Features.PaymentSystemWidgetFeatures.Commands;
using EVerywhere.ModulesCommon.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EVerywhere.Balance.API.Controllers;

/// <summary>
/// Обработка веб хуков BePaid
/// </summary>
/// <param name="sender"></param>
/// <param name="mediator"></param>
[ApiExplorerSettings(IgnoreApi = true)]
public class BePaidController(ISender sender, IMediator mediator)
    : MediatrController(sender, mediator)
{
    /// <summary>
    /// Обработка запроса о привязке карты
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [EnsureBePaid]
    [SetBePaidIsCurrentUser]
    [HttpPost("verifyPaymentMethodNotification")]
    public async Task<IActionResult> VerifyPaymentMethodNotification(CancellationToken cancellationToken)
    {
        var model = await BodyReaderHelper.ReadBody(HttpContext.Request, cancellationToken);

        await sender.Send(new ProcessCreatePaymentMethodWidgetResponseCommand { JsonResponse = model }, cancellationToken);
        
        return Ok();
    }

    /// <summary>
    /// Обработка запроса о платеже
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [EnsureBePaid]
    [SetBePaidIsCurrentUser]
    [HttpPost("verifyPaymentNotification")]
    public async Task<IActionResult> VerifyPaymentNotification(CancellationToken cancellationToken)
    {
        var model = await BodyReaderHelper.ReadBody(HttpContext.Request, cancellationToken);

        await sender.Send(new ProcessPaymentWidgetResponseCommand { JsonResponse = model }, cancellationToken);
        
        return Ok(); 
    }
}   