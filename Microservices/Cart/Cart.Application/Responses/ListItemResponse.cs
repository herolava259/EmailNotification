using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Responses
{
    public class ListItemResponse
    {
        
        public Guid Id { get; set; }
        public string ProductId { get; set; }

        public Guid CartId { get; set; }

        public int Amount { get; set; }
    }
}
