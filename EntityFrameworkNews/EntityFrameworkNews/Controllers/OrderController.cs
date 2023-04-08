using EntityFrameworkNews.Features.Order;
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

    [HttpDelete]
    public async Task<IActionResult> DeleteAllOrderOld(DeleteAllOrdersOldCommand request)
      => Ok(await _mediator.Send(request));

    [HttpPut]
    public async Task<IActionResult> UpdateOrderOld(UpdateOrderOldCommand request)
        => Ok(await _mediator.Send(request));


    [HttpPut]
    public async Task<IActionResult> UpdateAllOrdersOld(UpdateAllOrdersOldCommand request)
        => Ok(await _mediator.Send(request));

    #endregion Commands

    #region Queries

    [HttpPost]
    public async Task<IActionResult> GetAllOrdersOldWithoutRelations(GetAllOrdersOldWithoutRelationsQuery request)
       => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> GetAllOrdersOld(GetAllOrdersOldQuery request)
        => Ok(await _mediator.Send(request));

    #endregion Queries

    #endregion Old bad version

    #region New better version

    #region Commands

    [HttpDelete]
    public async Task<IActionResult> DeleteOrderNew(DeleteOrderNewCommand request)
    => Ok(await _mediator.Send(request));

    [HttpDelete]
    public async Task<IActionResult> DeleteAllOrderNew(DeleteAllOrdersNewCommand request)
     => Ok(await _mediator.Send(request));

    [HttpPut]
    public async Task<IActionResult> UpdateOrderNew(UpdateOrderNewCommand request)
        => Ok(await _mediator.Send(request));



    [HttpPut]
    public async Task<IActionResult> UpdateAllOrdersNew(UpdateAllOrdersNewCommand request)
        => Ok(await _mediator.Send(request));

    #endregion Commands

    #region Queries

    [HttpPost]
    public async Task<IActionResult> GetAllOrdersNew(GetAllOrdersNewQuery request)
      => Ok(await _mediator.Send(request));

    #endregion Queries

    #endregion New better version

    #region Single vs First

    [HttpPost]
    public async Task<IActionResult> RunSingleOrFirst(RunSingleOrFirstQuery request)
      => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> RunSingleOrFirstInList(RunSingleOrFirstInListQuery request)
      => Ok(await _mediator.Send(request));

    #endregion
}
