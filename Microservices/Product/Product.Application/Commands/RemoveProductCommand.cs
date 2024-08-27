using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Commands
{
    public class RemoveProductCommand: IRequest<bool>
    {
        public RemoveProductCommand(Guid productId)
        {
            ProductId = productId;
        }

        public Guid ProductId { get; }
    }
}
