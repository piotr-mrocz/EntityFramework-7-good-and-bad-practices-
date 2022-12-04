using EntityFrameworkNews.Features.Order.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkNews.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> GetAllOrdersOldWithoutRelations(GetAllOrdersOldWithoutRelationsQuery request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> GetAllOrdersOld(GetAllOrdersOldQuery request)
        => Ok(await _mediator.Send(request));
}
