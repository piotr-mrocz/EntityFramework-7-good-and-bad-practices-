using EntityFrameworkNews.Data;
using EntityFrameworkNews.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Features.Order;

public sealed class UpdateOrderOldCommand : IRequest<Response<string>>
{
    public int IdOrder { get; set; }
    public int IdUser { get; set; }
    public int IdProduct { get; set; }
}

public sealed class UpdateOrderOldHandler : IRequestHandler<UpdateOrderOldCommand, Response<string>>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateOrderOldHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<string>> Handle(UpdateOrderOldCommand request, CancellationToken cancellationToken)
    {
        var startTime = DateTime.Now;

        var order = await _dbContext.Orders
            .FirstOrDefaultAsync(x => x.Id == request.IdOrder, cancellationToken);

        if (order == null)
        {
            var endTimeError = DateTime.Now;
            var queryTimeError = $"{(endTimeError - startTime).TotalSeconds:N2} (w sekundach)";

            return new Response<string>(false, "Nie odnaleziono wpisu w bazie danych!", queryTimeError);
        }

        order.IdUser = request.IdUser;
        order.IdProduct = request.IdProduct;

        _dbContext.Orders.Entry(order).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);

        var endTime = DateTime.Now;
        var queryTime = $"{(endTime - startTime).TotalSeconds:N2} (w sekundach)";

        return new Response<string>(true, "Operacja zakończona powodzeniem", queryTime);
    }
}
