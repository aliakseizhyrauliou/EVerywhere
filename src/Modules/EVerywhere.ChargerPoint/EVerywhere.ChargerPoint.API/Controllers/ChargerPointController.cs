using EVerywhere.ChargerPoint.Application.Features.ChargerFeatures.Queries;
using EVerywhere.ModulesCommon.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EVerywhere.ChargerPoint.API.Controllers;

public class ChargerPointController(ISender sender, IMediator mediator) : MediatrController(sender, mediator)
{
    [HttpGet("list")]
    public async Task<List<ChargerDto>> List(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetChargerListQuery(), cancellationToken);
    }
}