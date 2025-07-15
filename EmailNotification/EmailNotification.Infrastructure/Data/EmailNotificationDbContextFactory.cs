using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EmailNotification.Infrastructure.Data;

public class EmailNotificationDbContextFactory : IDesignTimeDbContextFactory<EmailNotificationDBContext>
{
    public EmailNotificationDBContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<EmailNotificationDBContext>();

        optionBuilder.UseSqlServer("Data Source=UserDb");

        return new EmailNotificationDBContext(optionBuilder.Options);
    }
}
