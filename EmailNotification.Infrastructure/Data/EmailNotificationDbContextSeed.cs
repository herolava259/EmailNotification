
using EmailNotification.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EStatus = EmailNotification.Core.Enums.UserAccountEnum.EStatus;

namespace EmailNotification.Infrastructure.Data;

public static class EmailNotificationDbContextSeed
{
    public static async Task SeedAsync(EmailNotificationDBContext dbContext, ILogger<EmailNotificationDBContext> logger)
    {

        if(!(await dbContext.UserAccounts.AnyAsync()))
        {
            dbContext.UserAccounts.AddRange(GetUserAccounts());

            await dbContext.SaveChangesAsync();

            logger.LogInformation($"UserAccount table's seeded");
        }

        if (!(await dbContext.Profiles.AnyAsync()))
        {
            dbContext.Profiles.AddRange(GetProfiles());

            await dbContext.SaveChangesAsync();

            logger.LogInformation($"Profile table's seeded");
        }
    }

    public static IEnumerable<UserAccount> GetUserAccounts()
    {
        return new List<UserAccount>()
        {
            new UserAccount
            {
                Id = new Guid("a6904b79-28ac-45b8-9e85-5ec2a8e7994f"),
                Email = "chopperman249@gmail.com",
                Status = EStatus.Normal,
                LastUpdatePassword = DateTimeOffset.UtcNow.AddDays(-90)

            }
        };
    }

    public static IEnumerable<Profile> GetProfiles()
    {
        return new List<Profile>()
        {
            new Profile
            {
                Id = new Guid("161166ae-e2a5-4d21-baf7-807c09374b79"),
                FirstName = "Farrer",
                LastName = "Le",
                Address = "Vietnam",
                UserAccountId = new Guid("a6904b79-28ac-45b8-9e85-5ec2a8e7994f")
            }
        };
    }
}
