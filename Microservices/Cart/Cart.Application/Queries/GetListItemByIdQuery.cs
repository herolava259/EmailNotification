using Cart.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Queries
{
    public class GetListItemByIdQuery: IRequest<ListItemResponse>
    {
        public GetListItemByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; init; }
    }
}
