using EntityFrameworkNews.Data;
using EntityFrameworkNews.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Features.Order;

public sealed class UpdateOrderNewCommand : IRequest<Response<string>>
{
    public int IdOrder { get; set; }
    public int IdUser { get; set; }
    public int IdProduct { get; set; }
}

public sealed class UpdateOrderNewHandler : IRequestHandler<UpdateOrderNewCommand, Response<string>>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateOrderNewHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<string>> Handle(UpdateOrderNewCommand request, CancellationToken cancellationToken)
    {
        var startTime = DateTime.Now;

        var orderReturn = await _dbContext.Orders
            .Where(x => x.Id == request.IdOrder)
            .ExecuteUpdateAsync(x => 
                  x.SetProperty(u => u.IdUser, u => request.IdUser)
                  .SetProperty(p => p.IdProduct, p => request.IdProduct));

        var response = orderReturn > 0;
        var message = response ? "Operacja zakończona powodzeniem" : "Nie udało się zaktualizować wpisu";

        var endTime = DateTime.Now;
        var queryTime = $"{(endTime - startTime).TotalSeconds:N2} (w sekundach)";

        return new Response<string>(response, message, queryTime);
    }
}
