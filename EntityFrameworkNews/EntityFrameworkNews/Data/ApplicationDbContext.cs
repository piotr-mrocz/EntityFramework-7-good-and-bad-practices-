using EntityFrameworkNews.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
	public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
       => await base.SaveChangesAsync(cancellationToken);

    #region DbSets

    public DbSet<User> Users { get; set; }

    #endregion DbSets

    protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
	}
}
