using EntityFrameworkNews.Data;
using EntityFrameworkNews.Models.Dtos;
using EntityFrameworkNews.Models.Entities;
using EntityFrameworkNews.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Features.Order.Queries;

public sealed class GetAllOrdersNewQuery : IRequest<Response<List<OrderDto>>>
{
}

file sealed class GetAllOrdersNewHandler : IRequestHandler<GetAllOrdersNewQuery, Response<List<OrderDto>>>
{
    private readonly IApplicationDbContext _dbContet;

    public GetAllOrdersNewHandler(IApplicationDbContext dbContet)
    {
        _dbContet = dbContet;
    }

    public async Task<Response<List<OrderDto>>> Handle(GetAllOrdersNewQuery request, CancellationToken cancellationToken)
    {
        var startTime = DateTime.Now;

        var orders = await _dbContet.Orders
            .Include(x => x.User)
            .Include(x => x.Product)
            .AsNoTracking()
            .Select(order => new OrderDto()
            {
                Id = order.Id,
                Product = order.Product.ProductName,
                User = $"{order.User.FirstName} {order.User.LastName}"
            })
            .ToListAsync(cancellationToken);

        var endTime = DateTime.Now;

        var queryTime = (endTime - startTime).TotalSeconds;

        return new Response<List<OrderDto>>()
        {
            Success = true,
            Data = orders.Take(1).ToList(),
            QueryTime = $"{queryTime.ToString("N2")} (w sekundach)"
        };
    }
}
