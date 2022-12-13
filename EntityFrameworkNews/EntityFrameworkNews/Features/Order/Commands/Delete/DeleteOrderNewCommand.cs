using EntityFrameworkNews.Data;
using EntityFrameworkNews.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Features.Order;

public sealed class DeleteOrderNewCommand : IRequest<Response<string>>
{
    public int IdToDelete { get; set; }
}

public sealed class DeleteOrderNewHandler : IRequestHandler<DeleteOrderNewCommand, Response<string>>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteOrderNewHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<string>> Handle(DeleteOrderNewCommand request, CancellationToken cancellationToken)
    {
        var startTime = DateTime.Now;

        var response = await _dbContext.Orders
            .Where(x => x.Id == request.IdToDelete)
            .ExecuteDeleteAsync();

        var message = response == 1 ? "Udało się" : "Nie odnaleziono wpisu w bazie danych!";

        var endTime = DateTime.Now;
        var queryTime = $"{(endTime - startTime).TotalSeconds:N2} (w sekundach)";

        return new Response<string>(response == 1, message, queryTime);
    }
}
