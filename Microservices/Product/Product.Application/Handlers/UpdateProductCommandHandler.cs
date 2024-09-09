using AutoMapper;
using MediatR;
using Product.Application.Commands;
using Product.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProductEntity = Product.Core.Entities.Product;

namespace Product.Application.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductRepository productRepository,
                                      IMapper mapper)
    {
        this._productRepository = productRepository;
        this._mapper = mapper;
    }
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        if (!ValidateProduceName(request.ProductName))
            throw new ArgumentException($"Invalid Product Name {request.ProductName}");

        var updatingEntity = _mapper.Map<ProductEntity>(request);

        return await _productRepository.UpdateProduct(updatingEntity);
    }


    public bool ValidateProduceName(string produceName)
        => Regex.IsMatch(produceName, @"^([a-zA-Z0-9]{2,64}\s){0,15}[a-zA-Z0-9]{2,64}$");

}
