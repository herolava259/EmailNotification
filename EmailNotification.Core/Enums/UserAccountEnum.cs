using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotification.Core.Enums
{
    public static class UserAccountEnum
    {
        public enum EStatus
        {
            Normal = 100_000_000,
            RequireChangePassword= 100_000_001
        }
    }
}
