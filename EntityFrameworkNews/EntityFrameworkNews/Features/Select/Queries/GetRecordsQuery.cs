using EntityFrameworkNews.Consts;
using EntityFrameworkNews.Data;
using EntityFrameworkNews.Models.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Features.Select.Queries;

public record GetRecordsQuery : IRequest<List<string>>;

file sealed class GetRecordsHandler : IRequestHandler<GetRecordsQuery, List<string>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetRecordsHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<string>> Handle(GetRecordsQuery request, CancellationToken cancellationToken)
    {
        return new()
        {
            await GetOrdersListDto(cancellationToken),
            GetOrdersIEnumerableDto(),
            await GetOrdersListSkipTakeDto(cancellationToken),
            await GetOrdersIEnumerableSkipTake(cancellationToken)
        };
    }

    private async Task<string> GetOrdersListDto(CancellationToken cancellationToken)
    {
        var startForLoop = DateTime.Now;
        var orders = await GetOrdersList(cancellationToken);

        var total = orders.Count;

        var dto = orders
            .Skip(GridConst.Skip)
            .Take(GridConst.Take)
            .Select(x => new OrderDto()
            {
                Id = x.Id
            })
            .ToList();

        var endForLoop = DateTime.Now;
        var diffrentForLoop = (endForLoop - startForLoop).TotalSeconds;

        return $"GetOrdersListDto: {diffrentForLoop} [s]";
    }

    private async Task<List<Models.Entities.Order>> GetOrdersList(CancellationToken cancellationToken)
    {
        return await _dbContext.Orders
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    private string GetOrdersIEnumerableDto()
    {
        var startForLoop = DateTime.Now;
        var orders = GetOrdersIEnumerabe();

        var total = orders.Count();

        var dto = orders
            .ToList()
            .Skip(GridConst.Skip)
            .Take(GridConst.Take)
            .Select(x => new OrderDto()
            {
                Id = x.Id
            })
            .ToList();

        var endForLoop = DateTime.Now;
        var diffrentForLoop = (endForLoop - startForLoop).TotalSeconds;

        return $"GetOrdersIEnumerableDto: {diffrentForLoop} [s]";
    }

    private IEnumerable<Models.Entities.Order> GetOrdersIEnumerabe()
    {
        return _dbContext.Orders
            .AsNoTracking()
            .AsEnumerable();
    }

    private async Task<string> GetOrdersListSkipTakeDto(CancellationToken cancellationToken)
    {
        var startForLoop = DateTime.Now;
        var orders = await GetOrdersListSkipTake(cancellationToken);

        var total = await GetAllOrdersCount(cancellationToken);

        var dto = orders
            .Select(x => new OrderDto()
            {
                Id = x.Id
            })
            .ToList();

        var endForLoop = DateTime.Now;
        var diffrentForLoop = (endForLoop - startForLoop).TotalSeconds;

        return $"GetOrdersListSkipTakeDto: {diffrentForLoop} [s]";
    }

    private async Task<List<Models.Entities.Order>> GetOrdersListSkipTake(CancellationToken cancellationToken)
    {
        return await _dbContext.Orders
            .AsNoTracking()
            .Skip(GridConst.Skip)
            .Take(GridConst.Take)
            .ToListAsync(cancellationToken);
    }

    private async Task<int> GetAllOrdersCount(CancellationToken cancellationToken)
    {
        return await _dbContext.Orders
            .AsNoTracking()
            .CountAsync(cancellationToken);
    }

    private async Task<string> GetOrdersIEnumerableSkipTake(CancellationToken cancellationToken)
    {
        var startForLoop = DateTime.Now;
        var orders = GetOrdersIEnumerabeSkipTake();

        var total = await GetAllOrdersCount(cancellationToken);

        var dto = orders
            .ToList()
            .Select(x => new OrderDto()
            {
                Id = x.Id
            })
            .ToList();

        var endForLoop = DateTime.Now;
        var diffrentForLoop = (endForLoop - startForLoop).TotalSeconds;

        return $"GetOrdersIEnumerableSkipTake: {diffrentForLoop} [s]";
    }

    private IEnumerable<Models.Entities.Order> GetOrdersIEnumerabeSkipTake()
    {
        return _dbContext.Orders
            .AsNoTracking()
             .Skip(GridConst.Skip)
            .Take(GridConst.Take)
            .AsEnumerable();
    }
}
