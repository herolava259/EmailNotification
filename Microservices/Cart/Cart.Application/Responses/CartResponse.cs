using Cart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Responses
{
    public class CartResponse
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset UpdatedDate { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<ListItemResponse>? ListItems { get; set; }
    }
}
