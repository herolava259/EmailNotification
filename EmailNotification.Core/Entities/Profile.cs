using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotification.Core.Entities
{
    public class Profile : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }
    }
}
