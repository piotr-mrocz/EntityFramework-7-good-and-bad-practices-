using EntityFrameworkNews.Features.Select.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkNews.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]

public class SelectController : ControllerBase
{
    private readonly IMediator _mediator;

    public SelectController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> GetRecords(GetRecordsQuery request)
       => Ok(await _mediator.Send(request));
}
