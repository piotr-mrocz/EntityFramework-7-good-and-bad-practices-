using EntityFrameworkNews.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Data;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    DbSet<User> Users { get; set; }
}
