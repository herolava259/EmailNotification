

using MediatR;
using Product.Application.Responses;

namespace Product.Application.Queries;

public class GetProductByNameQuery: IRequest<ProductResponse>
{
    public GetProductByNameQuery(string name)
    {
        Name = name;
    }

    public string Name { get; }
}
