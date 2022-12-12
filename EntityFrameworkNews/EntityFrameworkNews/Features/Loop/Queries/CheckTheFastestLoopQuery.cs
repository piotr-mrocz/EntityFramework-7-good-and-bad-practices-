using EntityFrameworkNews.Data;
using EntityFrameworkNews.Models.Dtos;
using EntityFrameworkNews.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace EntityFrameworkNews.Features.Loop.Queries;

public sealed class CheckTheFastestLoopQuery : IRequest<List<string>>
{
}

file sealed class CheckTheFastestLoopHandler : IRequestHandler<CheckTheFastestLoopQuery, List<string>>
{
    private readonly IApplicationDbContext _dbContet;

    public CheckTheFastestLoopHandler(IApplicationDbContext dbContet)
    {
        _dbContet = dbContet;
    }

    public async Task<List<string>> Handle(CheckTheFastestLoopQuery request, CancellationToken cancellationToken)
    {
        var orders = await _dbContet.Orders.ToListAsync(cancellationToken);

        var idsUsers = orders.Select(x => x.IdUser).ToList();
        var idsProducts = orders.Select(x => x.IdProduct).ToList();

        var users = await _dbContet.Users
           .Where(x => idsUsers.Contains(x.Id))
           .ToListAsync(cancellationToken);

        var products = await _dbContet.Products
            .Where(x => idsProducts.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var response = new List<string>
        {
            GetTimeForLoop(orders, users, products),
            GetTimeForeachLoop(orders, users, products),
            GetTimeForSpanLoop(orders, users, products),
            GetTimeForeachSpanLoop(orders, users, products)
        };

        return response;
    }

    private string GetTimeForLoop(
        List<Models.Entities.Order> orders,
        List<User> users,
        List<Product> products)
    {
        var startForLoop = DateTime.Now;

        for (int i = 0; i < orders.Count; i++)
        {
            GetUsersandProducts(orders[i], users, products);
        }

        var endForLoop = DateTime.Now;
        var diffrentForLoop = (endForLoop - startForLoop).TotalSeconds;

        diffrentForLoop = diffrentForLoop < 0 ? diffrentForLoop * -1 : diffrentForLoop;
        return $"For loop: {diffrentForLoop} [s]";
    }

    private string GetTimeForeachLoop(
        List<Models.Entities.Order> orders,
        List<User> users,
        List<Product> products)
    {
        var startForeachLoop = DateTime.Now;

        foreach (var order in orders)
        {
           GetUsersandProducts(order, users, products);
        }

        var endForeachLoop = DateTime.Now;
        var diffrentForeachLoop = (endForeachLoop - startForeachLoop).TotalSeconds;

        diffrentForeachLoop = diffrentForeachLoop < 0 ? diffrentForeachLoop * -1 : diffrentForeachLoop;
        return $"Foreach loop: {diffrentForeachLoop} [s]";
    }

    private string GetTimeForSpanLoop(
        List<Models.Entities.Order> orders,
        List<User> users,
        List<Product> products)
    {
        var asSpan = GetOrdersAsSpan(orders);
        var startForSpanLoop = DateTime.Now;

        for (int i = 0; i < asSpan.Length; i++)
        {
            GetUsersandProducts(asSpan[i], users, products);
        }

        var endForSpanLoop = DateTime.Now;
        var diffrentForSpanLoop = (endForSpanLoop - startForSpanLoop).TotalSeconds;

        diffrentForSpanLoop = diffrentForSpanLoop < 0 ? diffrentForSpanLoop * -1 : diffrentForSpanLoop;
        return $"For span loop: {diffrentForSpanLoop} [s]";
    }

    private string GetTimeForeachSpanLoop(
        List<Models.Entities.Order> orders,
        List<User> users,
        List<Product> products)
    {
        var asSpan = GetOrdersAsSpan(orders);
        var startForeachSpanLoop = DateTime.Now;

        foreach (var order in asSpan)
        {
            GetUsersandProducts(order, users, products);
        }

        var endForeachSpanLoop = DateTime.Now;
        var diffrentForeachSpanLoop = (endForeachSpanLoop - startForeachSpanLoop).TotalSeconds;

        diffrentForeachSpanLoop = diffrentForeachSpanLoop < 0 ? diffrentForeachSpanLoop * -1 : diffrentForeachSpanLoop;
        return $"Foreach span loop: {diffrentForeachSpanLoop} [s]";
    }

    private Span<Models.Entities.Order> GetOrdersAsSpan(List<Models.Entities.Order> orders)
        => CollectionsMarshal.AsSpan(orders);

    // do this because it must takes a little time to compare loops
    private OrderDto GetUsersandProducts(
        Models.Entities.Order order, 
        List<User> users,
        List<Product> products)
    {
        var user = users.FirstOrDefault(x => x.Id == order.IdUser);
        var product = products.FirstOrDefault(x => x.Id == order.IdProduct);

        return new OrderDto()
        {
            Id = order.Id,
            User = user != null ? $"{user.FirstName} {user.LastName}" : string.Empty,
            Product = product != null ? product.ProductName : string.Empty
        };
    }
}
