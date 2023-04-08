using EntityFrameworkNews.Data;
using EntityFrameworkNews.Models.Dtos;
using EntityFrameworkNews.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Features.Order;

public sealed class RunSingleOrFirstQuery : IRequest<Response<OrderDto>>
{
    public int IdOrder { get; set; }
}

file sealed class RunSingleOrFirstHadler : IRequestHandler<RunSingleOrFirstQuery, Response<OrderDto>>
{
    private readonly IApplicationDbContext _dbContet;
    private readonly ApplicationDbContext _appDbContext;

    public RunSingleOrFirstHadler(IApplicationDbContext dbContet, ApplicationDbContext appDbContext)
    {
        _dbContet = dbContet;
        _appDbContext = appDbContext;
    }

    public async Task<Response<OrderDto>> Handle(RunSingleOrFirstQuery request, CancellationToken cancellationToken)
    {
        var single = await SingleOrDefault(request.IdOrder, cancellationToken);
        var first = await FirstOrDefault(request.IdOrder, cancellationToken);
        var singleCompile = await SingleOrDefaultCompile(request.IdOrder);
        var firstCompile = await FirstOrDefaultCompile(request.IdOrder);

        return new Response<OrderDto>()
        {
            Success = true,
            QueryTime = $"{single}{Environment.NewLine}{singleCompile}{Environment.NewLine}{first}{Environment.NewLine}{firstCompile}"
        };
    }

    private async Task<string> SingleOrDefault(int idOrder, CancellationToken cancellationToken)
    {
        var startTime = DateTime.Now;

        var order = await _dbContet.Orders
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == idOrder, cancellationToken);

        var endTime = DateTime.Now;

        var queryTime = (endTime - startTime).TotalSeconds;

        return $"SingleOrDefault: {queryTime}";
    }

    private async Task<string> FirstOrDefault(int idOrder, CancellationToken cancellationToken)
    {
        var startTime = DateTime.Now;

        var order = await _dbContet.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == idOrder, cancellationToken);

        var endTime = DateTime.Now;

        var queryTime = (endTime - startTime).TotalSeconds;

        return $"FirstOrDefault: {queryTime}";
    }

    private static readonly Func<ApplicationDbContext, int, Task<Models.Entities.Order?>> SingleOrderAsync =
        EF.CompileAsyncQuery(
            (ApplicationDbContext context, int id) =>
                context.Orders.SingleOrDefault(x => x.Id == id));

    private async Task<string> SingleOrDefaultCompile(int idOrder)
    {
        var startTime = DateTime.Now;

        var order = await SingleOrderAsync(_appDbContext, idOrder);

        var endTime = DateTime.Now;

        var queryTime = (endTime - startTime).TotalSeconds;

        return $"SingleOrDefaultCompile: {queryTime}";
    }

    private static readonly Func<ApplicationDbContext, int, Task<Models.Entities.Order?>> FirstOrderAsync =
        EF.CompileAsyncQuery(
            (ApplicationDbContext context, int id) =>
                context.Orders.FirstOrDefault(x => x.Id == id));

    private async Task<string> FirstOrDefaultCompile(int idOrder)
    {
        var startTime = DateTime.Now;

        var order = await FirstOrderAsync(_appDbContext, idOrder);

        var endTime = DateTime.Now;

        var queryTime = (endTime - startTime).TotalSeconds;

        return $"FirstOrDefaultCompile: {queryTime}";
    }
}
