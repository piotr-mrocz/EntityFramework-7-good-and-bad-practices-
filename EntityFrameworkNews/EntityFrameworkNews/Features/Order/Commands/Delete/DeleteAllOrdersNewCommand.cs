using EntityFrameworkNews.Data;
using EntityFrameworkNews.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Features.Order;

public sealed class DeleteAllOrdersNewCommand : IRequest<Response<string>>
{
}

file sealed class DeleteAllOrdersNewHandler : IRequestHandler<DeleteAllOrdersNewCommand, Response<string>>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteAllOrdersNewHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<string>> Handle(DeleteAllOrdersNewCommand request, CancellationToken cancellationToken)
    {
        var startTime = DateTime.Now;

        var response = await _dbContext.Orders
            .ExecuteDeleteAsync(cancellationToken);

        var message = response == 1 ? "Udało się" : "Nie udało się usunąć wpisów!";

        var endTime = DateTime.Now;
        var queryTime = $"{(endTime - startTime).TotalSeconds:N2} (w sekundach)";

        return new Response<string>(response == 1, message, queryTime);
    }
}
