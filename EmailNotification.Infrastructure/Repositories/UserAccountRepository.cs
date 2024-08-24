using EmailNotification.Core.Entities;
using EmailNotification.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotification.Infrastructure.Repositories
{
    public class UserAccountRepository : BaseRepository<UserAccount>, IUserAccountRepository
    {
        public Task<bool> RequireToChangePassword(IEnumerable<UserAccount> accounts)
        {
            throw new NotImplementedException();
        }
    }
}
