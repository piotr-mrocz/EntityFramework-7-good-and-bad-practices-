using EntityFrameworkNews.Features.Order.Commands;
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

    #region Old bad version

    #region Commands

    [HttpDelete]
    public async Task<IActionResult> DeleteOrderOld(DeleteOrderOldCommand request)
      => Ok(await _mediator.Send(request));

    #endregion Commands

    #region Queries

    [HttpGet]
    public async Task<IActionResult> GetAllOrdersOldWithoutRelations(GetAllOrdersOldWithoutRelationsQuery request)
       => Ok(await _mediator.Send(request));

    [HttpGet]
    public async Task<IActionResult> GetAllOrdersOld(GetAllOrdersOldQuery request)
        => Ok(await _mediator.Send(request));

    #endregion Queries

    #endregion Old bad version

    #region New better version

    #region Commands

    [HttpDelete]
    public async Task<IActionResult> DeleteOrderNew(DeleteOrderNewCommand request)
    => Ok(await _mediator.Send(request));

    #endregion Commands

    #region Queries

    [HttpGet]
    public async Task<IActionResult> GetAllOrdersNew(GetAllOrdersNewQuery request)
      => Ok(await _mediator.Send(request));

    #endregion Queries

    #endregion New better version
}
