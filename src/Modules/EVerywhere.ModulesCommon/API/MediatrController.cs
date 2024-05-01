using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EVerywhere.ModulesCommon.API;

[ApiController]
[Route("api/[controller]")]
public class MediatrController(ISender sender, IMediator mediator) : ControllerBase
{
    protected IMediator _mediator = mediator;
    protected ISender _sender = sender;
}