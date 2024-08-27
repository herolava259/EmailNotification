using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Commands
{
    public class DeleteCartByIdCommand: IRequest<bool>
    {
        public DeleteCartByIdCommand(Guid id)
        {
            CartId = id;
        }

        public Guid CartId { get; }
    }
}
