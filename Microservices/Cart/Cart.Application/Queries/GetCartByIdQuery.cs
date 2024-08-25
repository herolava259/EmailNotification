using Cart.Application.Responses;
using MediatR;

namespace Cart.Application.Queries
{
    public class GetCartByIdQuery: IRequest<CartResponse>
    {
        public GetCartByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; init; }
    }
}
