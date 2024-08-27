using EmailNotification.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotification.Core.Repositories
{
    public interface IUserAccountRepository : IBaseRepository<UserAccount>
    {
        public Task<bool> RequireToChangePassword(IEnumerable<UserAccount> accounts);

        public Task<List<UserAccount>> GetUserAccountsNeedToChangePassword(DateTimeOffset expiredDate);
    }
}
