using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Core.Entities
{
    public class Cart: BaseEntity
    {
        public decimal TotalPrice { get; set; }

        public ICollection<ListItem>? ListItems { get; set; }
    }
}
