using EntityFrameworkNews.Data;
using EntityFrameworkNews.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Features.Order;

public sealed class DeleteAllOrdersOldCommand : IRequest<Response<string>>
{
}

file sealed class DeleteAllOrdersOldandler : IRequestHandler<DeleteAllOrdersOldCommand, Response<string>>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteAllOrdersOldandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<string>> Handle(DeleteAllOrdersOldCommand request, CancellationToken cancellationToken)
    {
        var startTime = DateTime.Now;

        var orders = await _dbContext.Orders
           .ToListAsync(cancellationToken);

        foreach (var order in orders)
            _dbContext.Orders.Entry(order).State = EntityState.Deleted;

       var response = await _dbContext.SaveChangesAsync(cancellationToken);

        var endTime = DateTime.Now;
        var queryTime = $"{(endTime - startTime).TotalSeconds:N2} (w sekundach)";

        var success = response == 1;
        var message = success ? "Operacja zakończona pomyślnie" : "Nie udało się usunąć wpisów";

        return new Response<string>(success, message, queryTime);
    }
}
