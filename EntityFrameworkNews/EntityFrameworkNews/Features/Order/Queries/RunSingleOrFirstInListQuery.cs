using EntityFrameworkNews.Data;
using EntityFrameworkNews.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Features.Order;

public sealed class RunSingleOrFirstInListQuery : IRequest<Response<string>>
{
    public int NumberOfElements { get; set; }
}

file sealed class RunSingleOrFirstInListHandler : IRequestHandler<RunSingleOrFirstInListQuery, Response<string>>
{
    private readonly IApplicationDbContext _dbContext;

    private const int _entityPosition = 20000;

    public RunSingleOrFirstInListHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<string>> Handle(RunSingleOrFirstInListQuery request, CancellationToken cancellationToken)
    {
        var ordersList = await GetListOfOrders(request.NumberOfElements, cancellationToken);

        var single = SingleOrDefault(ordersList);
        var first = FirstOrDefault(ordersList);

        return new()
        {
            Success = true,
            QueryTime = $"{single}{Environment.NewLine}{first}"
        };
    }

    private async Task<List<Models.Entities.Order>> GetListOfOrders(
        int numberOfElements,
        CancellationToken cancellationToken)
    {
        List<Models.Entities.Order> orderList = new();

        var idsUsers = await _dbContext.Users.Select(x => x.Id).ToListAsync(cancellationToken);
        var idsProducts = await _dbContext.Products.Select(x => x.Id).ToListAsync(cancellationToken);

        var random = new Random();

        for (int number = 1; number <= numberOfElements; number++)
        {
            var randomIdProduct = random.Next(idsProducts.Count);
            var randomIdUser = random.Next(idsUsers.Count);

            var idProduct = idsProducts.FirstOrDefault(x => x == randomIdProduct);
            var idUser = idsUsers.FirstOrDefault(x => x == randomIdUser);

            orderList.Add(new()
            {
                IdProduct = idProduct != default ? idProduct : idsProducts.FirstOrDefault(),
                IdUser = idUser != default ? idUser : idsUsers.FirstOrDefault()
            });
        }

        return orderList;
    }

    private string SingleOrDefault(List<Models.Entities.Order> orderList)
    {
        var startTime = DateTime.Now;

        var order = orderList
            .SingleOrDefault(x => x.Id == _entityPosition);

        var endTime = DateTime.Now;

        var queryTime = (endTime - startTime).TotalSeconds;

        return $"SingleOrDefault: {queryTime}";
    }

    private string FirstOrDefault(List<Models.Entities.Order> orderList)
    {
        var startTime = DateTime.Now;

        var order = orderList
            .FirstOrDefault(x => x.Id == _entityPosition);

        var endTime = DateTime.Now;

        var queryTime = (endTime - startTime).TotalSeconds;

        return $"FirstOrDefault: {queryTime}";
    }
}
