using Microsoft.AspNetCore.Mvc;
using MediatR;
using EntityFrameworkNews.Features.Loop.Queries;

namespace EntityFrameworkNews.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]

public class LoopController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoopController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CheckTheFastestLoop(CheckTheFastestLoopQuery request)
       => Ok(await _mediator.Send(request));
}
