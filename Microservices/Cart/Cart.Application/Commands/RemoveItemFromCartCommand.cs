using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Commands
{
    public class RemoveItemFromCartCommand: IRequest<bool>
    {
        public RemoveItemFromCartCommand(Guid listItemId, decimal priceOfItem)
        {
            ListItemId = listItemId;
            PriceOfItem = priceOfItem;
        }

        public Guid ListItemId { get; }
        public decimal PriceOfItem { get; }
    }
}
