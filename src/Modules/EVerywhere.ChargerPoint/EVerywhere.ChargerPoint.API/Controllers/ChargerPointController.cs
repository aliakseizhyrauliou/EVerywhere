using EVerywhere.ChargerPoint.Application.Features.ChargerFeatures.Commands;
using EVerywhere.ChargerPoint.Application.Features.ChargerFeatures.Queries;
using EVerywhere.ModulesCommon.API;
using EVerywhere.ModulesCommon.UseCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EVerywhere.ChargerPoint.API.Controllers;

public class ChargerPointController(ISender sender, IMediator mediator) : MediatrController(sender, mediator)
{

    [HttpPost("create")]
    public async Task<CreatedEntityDto<long>> Create(CreateChargerCommand command, CancellationToken cancellationToken)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    [HttpGet("list")]
    public async Task<List<ChargerDto>> List(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetChargerListQuery(), cancellationToken);
    }
}