

using Cart.Core.Entities;
using Cart.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using CartEntity = Cart.Core.Entities.Cart;

namespace Cart.Infrastructure.Data;

public class CartDBContext: DbContext
{
    public DbSet<CartEntity> Carts { get; set; }

    public DbSet<ListItem> ListItems { get; set; }

    public CartDBContext(DbContextOptions<CartDBContext> options) : base(options)
    { }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTimeOffset.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedDate = DateTimeOffset.UtcNow;
                    break;
            }
        }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        try
        {
            modelBuilder.ApplyConfiguration(new CartFluentConfiguration());
            modelBuilder.ApplyConfiguration(new ListItemFluentConfiguration());
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.ToString());
        }
    }
}
