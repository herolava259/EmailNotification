using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Scheduler.Web.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity => 
        {
            entity.Property(e => e.EnableNotifications).HasDefaultValue(true);
            entity.Property(e => e.Initials).HasMaxLength(5);
        });

        builder.HasDefaultSchema("identity");
    }
}
