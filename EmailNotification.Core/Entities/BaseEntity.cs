using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotification.Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset UpdatedDate { get; set; }

        //[NotMapped]
        //public List<string> ModifyParams { get; private init; } = new(); 

    }
}
