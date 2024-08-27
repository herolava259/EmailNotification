

using EmailNotification.Core.Entities;
using EmailNotification.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace EmailNotification.Infrastructure.Data;

public class EmailNotificationDBContext : DbContext
{
    public DbSet<UserAccount> UserAccounts { get; set; }

    public DbSet<Profile> Profiles { get; set; }


    public EmailNotificationDBContext(DbContextOptions<EmailNotificationDBContext> options): base(options)
    {}


    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        foreach(var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch(entry.State)
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
            modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
        }
        catch (Exception ex) 
        {

            Console.WriteLine(ex.ToString());
        }
    }
}
