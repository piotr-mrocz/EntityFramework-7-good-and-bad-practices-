using EntityFrameworkNews.Data;
using EntityFrameworkNews.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Features.Order;

public sealed class UpdateAllOrdersOldCommand : IRequest<Response<string>>
{
}

file sealed class UpdateAllOrdersOldHandler : IRequestHandler<UpdateAllOrdersOldCommand, Response<string>>
{
    private readonly IApplicationDbContext _dbContext;
    private const int _defaultUserId = 3;
    private const int _defaultProductId = 3;

    public UpdateAllOrdersOldHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<string>> Handle(UpdateAllOrdersOldCommand request, CancellationToken cancellationToken)
    {
        var startTime = DateTime.Now;

        var orders = await _dbContext.Orders
            .ToListAsync(cancellationToken);

        foreach (var order in orders)
        {
            order.IdUser = _defaultUserId;
            order.IdProduct = _defaultProductId;

            _dbContext.Orders.Entry(order).State = EntityState.Modified;
        }

        var response = await _dbContext.SaveChangesAsync(cancellationToken);

        var endTime = DateTime.Now;
        var queryTime = $"{(endTime - startTime).TotalSeconds:N2} (w sekundach)";

        var success = response == 1;
        var message = success ? "Operacja zakończona pomyślnie" : "Nie udało się zaktualizować wpisów";

        return new Response<string>(success, message, queryTime);
    }
}
