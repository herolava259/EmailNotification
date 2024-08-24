using EmailNotification.Core.Entities;
using EmailNotification.Core.Repositories;
using EmailNotification.Infrastructure.Data;


namespace EmailNotification.Infrastructure.Repositories;

public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
{
    public ProfileRepository(EmailNotificationDBContext dbContext) : base(dbContext)
    {
    }
}
