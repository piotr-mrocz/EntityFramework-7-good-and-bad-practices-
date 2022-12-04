using EntityFrameworkNews.Data;
using EntityFrameworkNews.Models.Dtos;
using EntityFrameworkNews.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Features.Order.Queries;

public class GetAllOrdersOldQuery : IRequest<Response<List<OrderDto>>>
{
}

file class GetAllOrdersOldHandler : IRequestHandler<GetAllOrdersOldQuery, Response<List<OrderDto>>>
{
    private readonly IApplicationDbContext _dbContet;

    public GetAllOrdersOldHandler(IApplicationDbContext dbContet)
    {
        _dbContet = dbContet;
    }

    public async Task<Response<List<OrderDto>>> Handle(GetAllOrdersOldQuery request, CancellationToken cancellationToken)
    {
        var startTime = DateTime.Now;

        var orders = await _dbContet.Orders
            .Include(x => x.User)
            .Include(x => x.Product)
            .ToListAsync(cancellationToken);

        var ordersListDto = new List<OrderDto>();

        foreach (var order in orders)
        {
            ordersListDto.Add(new OrderDto()
            {
                Id = order.Id,
                Product = order.Product.ProductName,
                User = $"{order.User.FirstName} {order.User.LastName}"
            });
        }

        var endTime = DateTime.Now;

        var queryTime = (endTime - startTime).TotalSeconds;

        return new Response<List<OrderDto>>()
        {
            Success = true,
            Data = ordersListDto.Take(10).ToList(),
            QueryTime = $"{queryTime.ToString("N2")} (w sekundach)"
        };
    }
}
