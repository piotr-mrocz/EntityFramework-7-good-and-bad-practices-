using EntityFrameworkNews.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNews.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext() : base() { }

    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
       => await base.SaveChangesAsync(cancellationToken);

    public override int SaveChanges()
        => base.SaveChanges();

    #region DbSets

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    #endregion DbSets

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Order>(order =>
        {
            order.HasOne(p => p.Product)
            .WithMany(p => p.Orders)
            .HasForeignKey(x => x.IdProduct);

            order.HasOne(u => u.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(x => x.IdUser);
        });

        base.OnModelCreating(builder);
    }
}
