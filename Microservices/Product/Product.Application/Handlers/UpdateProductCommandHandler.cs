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

        if (!ValidatePriceOfProduct(request.Price))
            throw new ArgumentException($"Price is invalid: {request.Price}");

        if (!ValidateQuantityOfProduct(request.Quantity))
            throw new ArgumentException($"Quantity is invalid: {request.Price}");
        var updatingEntity = _mapper.Map<ProductEntity>(request);

        return await _productRepository.UpdateProduct(updatingEntity);
    }


    public bool ValidateProduceName(string produceName)
        => Regex.IsMatch(produceName, @"^([a-zA-Z0-9]{2,64}\s){0,15}[a-zA-Z0-9]{2,64}$");

    public bool ValidatePriceOfProduct(decimal price)
        => price >= 0 && price < 1_000_000;

    public bool ValidateQuantityOfProduct(int quantity)
        => quantity >= 0 && quantity <= 1_00_000;
}
