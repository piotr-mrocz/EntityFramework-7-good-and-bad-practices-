using EntityFrameworkNews.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Data;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    DbSet<User> Users { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Order> Orders { get; set; }
}
