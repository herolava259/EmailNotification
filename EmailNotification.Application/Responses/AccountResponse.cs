using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotification.Application.Responses
{
    public class AccountResponse
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset UpdatedDate { get; set; }

        public string Email { get; set; }

        public DateTimeOffset LastUpdatePassword { get; set; }
    }
}
