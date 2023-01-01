using EntityFrameworkNews.Data;
using EntityFrameworkNews.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Features.Order;

public sealed class UpdateAllOrdersNewCommand : IRequest<Response<string>>
{
}

file sealed class UpdateAllOrdersNewHandler : IRequestHandler<UpdateAllOrdersNewCommand, Response<string>>
{
    private readonly IApplicationDbContext _dbContext;
    private const int _defaultUserId = 4;
    private const int _defaultProductId = 4;

    public UpdateAllOrdersNewHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<string>> Handle(UpdateAllOrdersNewCommand request, CancellationToken cancellationToken)
    {
        var startTime = DateTime.Now;

        var orderReturn = await _dbContext.Orders
            .ExecuteUpdateAsync(x =>
                  x.SetProperty(u => u.IdUser, u => _defaultUserId)
                  .SetProperty(p => p.IdProduct, p => _defaultProductId));

        var response = orderReturn > 0;
        var message = response ? "Operacja zakończona powodzeniem" : "Nie udało się zaktualizować wpisu";

        var endTime = DateTime.Now;
        var queryTime = $"{(endTime - startTime).TotalSeconds:N2} (w sekundach)";

        return new Response<string>(response, message, queryTime);
    }
}
