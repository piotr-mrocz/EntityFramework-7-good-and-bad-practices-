using EntityFrameworkNews.Data;
using EntityFrameworkNews.Models.Dtos;
using EntityFrameworkNews.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Features.Order;

public sealed class GetAllOrdersOldWithoutRelationsQuery : IRequest<Response<List<OrderDto>>>
{
}

file sealed class GetAllOrdersOldWithoutRelationsHandler : IRequestHandler<GetAllOrdersOldWithoutRelationsQuery, Response<List<OrderDto>>>
{
    private readonly IApplicationDbContext _dbContet;

    public GetAllOrdersOldWithoutRelationsHandler(IApplicationDbContext dbContet)
    {
        _dbContet = dbContet;
    }

    public async Task<Response<List<OrderDto>>> Handle(GetAllOrdersOldWithoutRelationsQuery request, CancellationToken cancellationToken)
    {
        var startTime = DateTime.Now;

        var orders = await _dbContet.Orders
            .ToListAsync(cancellationToken);

        var ordersListDto = new List<OrderDto>();

        var idsProducts = orders.Select(x => x.IdProduct).Distinct().ToList();
        var idsUsers = orders.Select(x => x.IdUser).Distinct().ToList();

        var products = await _dbContet.Products
            .Where(x => idsProducts.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var users = await _dbContet.Users
            .Where(x => idsUsers.Contains(x.Id))
            .ToListAsync(cancellationToken);

        foreach (var order in orders)
        {
            var product = products.FirstOrDefault(x => x.Id == order.IdProduct);
            var user = users.FirstOrDefault(x => x.Id == order.IdUser);

            ordersListDto.Add(new OrderDto()
            {
                Id = order.Id,
                Product = product.ProductName,
                User = $"{user.FirstName} {user.LastName}"
            });
        }

        var endTime = DateTime.Now;

        var queryTime = (endTime - startTime).TotalSeconds;

        return new Response<List<OrderDto>>()
        {
            Success = true,
            Data = ordersListDto.Take(1).ToList(),
            QueryTime = $"{queryTime.ToString("N2")} (w sekundach)"
        };
    }
}
