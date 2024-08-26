using MediatR;
using Product.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Queries
{
    public class GetProductByIdQuery: IRequest<ProductResponse>
    {
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
