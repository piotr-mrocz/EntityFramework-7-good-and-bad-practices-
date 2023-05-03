using EntityFrameworkNews.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Data;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();

    DbSet<User> Users { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Order> Orders { get; set; }
}
