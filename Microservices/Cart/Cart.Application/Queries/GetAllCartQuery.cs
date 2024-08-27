using Cart.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Queries
{
    public class GetAllCartQuery: IRequest<IList<CartResponse>>
    {
        public GetAllCartQuery(int pageIndex, int pageSize, bool includeItem = false)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            IncludeItem = includeItem;
        }

        public int PageIndex { get; }
        public int PageSize { get; }
        public bool IncludeItem { get; }
    }
}
