using EVerywhere.Balance.Application.Features.PaidResourseTypeFeatures.Commands;
using EVerywhere.Balance.Application.Features.PaidResourseTypeFeatures.Queries;
using EVerywhere.ModulesCommon.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EVerywhere.Balance.API.Controllers;

/// <summary>
/// Контроллер для работы с типами платных ресурсов
/// </summary>
/// <param name="sender"></param>
/// <param name="mediator"></param>
public class PaidResourceTypeController(ISender sender, IMediator mediator) : MediatrController(sender, mediator)
{
    /// <summary>
    /// Создать платный ресурс
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost("create")]
    public async Task Create(CreatePaidResourceTypeCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Получить коллекцию платных ресурсов
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<List<PaidResourceTypeDto>> List(CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetPaidResourceTypesQuery(), cancellationToken);
    }

}