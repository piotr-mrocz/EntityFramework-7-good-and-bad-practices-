using EntityFrameworkNews.Data;
using EntityFrameworkNews.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Features.Order.Commands;

public sealed class DeleteOrderOldCommand : IRequest<Response<string>>
{
    public int IdToDelete { get; set; }
}

public sealed class DeleteOrderOldHandler : IRequestHandler<DeleteOrderOldCommand, Response<string>>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteOrderOldHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<string>> Handle(DeleteOrderOldCommand request, CancellationToken cancellationToken)
    {
        var startTime = DateTime.Now;

        var order = await _dbContext.Orders
            .FirstOrDefaultAsync(x => x.Id == request.IdToDelete, cancellationToken);

        if (order == null)
        {
            var endTimeError = DateTime.Now;
            var queryTimeError = $"{(endTimeError - startTime).TotalSeconds:N2} (w sekundach)";
            
            return new Response<string>(false, "Nie odnaleziono wpisu w bazie danych!", queryTimeError);
        }

        _dbContext.Orders.Entry(order).State = EntityState.Deleted;
        await _dbContext.SaveChangesAsync(cancellationToken);

        var endTime = DateTime.Now;
        var queryTime = $"{(endTime - startTime).TotalSeconds:N2} (w sekundach)";

        return new Response<string>(true, "", queryTime);
    }
}
