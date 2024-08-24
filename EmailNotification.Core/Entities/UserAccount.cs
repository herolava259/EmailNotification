using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotification.Core.Entities
{
    public class UserAccount: BaseEntity
    {
        public string Email { get; set; }

        public int MyProperty { get; set; }
        public Guid ProfileId { get; set; }

        public Profile Profile { get; set; }
    }
}
