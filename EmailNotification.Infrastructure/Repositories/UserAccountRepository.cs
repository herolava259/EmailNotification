using EmailNotification.Core.Entities;
using EmailNotification.Core.Repositories;
using EmailNotification.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EStatus = EmailNotification.Core.Enums.UserAccountEnum.EStatus;
namespace EmailNotification.Infrastructure.Repositories
{
    public class UserAccountRepository : BaseRepository<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(EmailNotificationDBContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<UserAccount>> GetUserAccountsNeedToChangePassword(DateTimeOffset expiredDate)
        {
            var accountQuery = _dbContext.UserAccounts.AsNoTracking().Where(c => c.LastUpdatePassword < expiredDate.AddMonths(-6)
                                                                                && c.Status == EStatus.Normal);
            return await accountQuery.Include(c => c.Profile).ToListAsync();
        }
        public async Task<bool> RequireToChangePassword(IEnumerable<UserAccount> accounts)
        {
            foreach (var item in accounts)
            {
                item.Status = EStatus.RequireChangePassword;
            }

            base._dbContext.AttachRange(accounts);

            foreach (var item in accounts)
            {
                base._dbContext.Entry(item).Property(nameof(item.LastUpdatePassword)).IsModified = true;
            }

            return (await _dbContext.SaveChangesAsync()) > 0;
        }

        
    }
}
