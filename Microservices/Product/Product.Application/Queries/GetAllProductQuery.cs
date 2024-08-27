using MediatR;
using Product.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Queries
{
    public class GetAllProductQuery: IRequest<IList<ProductResponse>>
    {
        public GetAllProductQuery() { }
    }
}
