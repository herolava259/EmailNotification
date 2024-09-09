using AutoMapper;
using Moq;
using NUnit.Framework;
using Product.Application.Commands;
using Product.Application.Handlers;
using Product.Core.Repositories;
using ProductEntity = Product.Core.Entities.Product;

namespace Product.UnitTests.Production.Application.Tests;

[TestFixture]
public class UpdateProductCommandHandlerTests
{
    private UpdateProductCommandHandler _handler;
    private Mock<IProductRepository> _repository;
    private Mock<IMapper> _mapper;

    
    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<IProductRepository>();
        _mapper = new Mock<IMapper>();

        _handler = new UpdateProductCommandHandler(_repository.Object, _mapper.Object);
        
    }

    [Test]
    [TestCase("Niceee Nike",200,1)]
    [TestCase("Adida Buddie Or Adidas", 1000, 2)]
    [TestCase("Louis Vuitton or Louis Pham", 1200, 9)]
    public async Task Handle_Should_Return_True_When_InputIsValidProduct(string name, 
                                                                    decimal price, int quantity)
    {
        var cmd = new UpdateProductCommand(Guid.NewGuid(), name, price, quantity);

        _repository.Setup(c => c.UpdateProduct(It.IsAny<ProductEntity>()))
                   .Returns(Task.FromResult(true));

        _mapper.Setup(c => c.Map<ProductEntity>(cmd))
               .Returns(new ProductEntity
               {
                   Id = cmd.Id,
                   Price = cmd.Price,
                   ProductName = cmd.ProductName,
                   Quantity = cmd.Quantity
               });

        var result = await _handler.Handle(cmd, default);

        Assert.True(result);

    }

    [Test]
    public void Handle_Should_Throw_ArgumentException_When_ProductNameInputIsEmpty()
    {
        var cmd = new UpdateProductCommand(Guid.NewGuid(), String.Empty, 100, 2);

        _repository.Setup(c => c.UpdateProduct(new()
        {
            Id = cmd.Id,
            Price = cmd.Price,
            ProductName = cmd.ProductName,
            Quantity = cmd.Quantity
        }))
                   .ReturnsAsync(false);

        _mapper.Setup(c => c.Map<ProductEntity>(cmd))
               .Returns(new ProductEntity
               {
                   Id = cmd.Id,
                   Price = cmd.Price,
                   ProductName = cmd.ProductName,
                   Quantity = cmd.Quantity
               });


        Assert.ThrowsAsync<ArgumentException>(async () => await _handler.Handle(cmd, default));


    }

    [Test]
    [TestCase("g$h kaka#")]
    [TestCase("jjkkl**** 8**")]
    [TestCase("**JJ **jk")]
    public void Handle_Should_Throw_ArgumentException_When_ProductNameContainInvalidCharacters(string productName)
    {
        var cmd = new UpdateProductCommand(Guid.NewGuid(), productName, 100, 2);

        _repository.Setup(c => c.UpdateProduct(new()
        {
            Id = cmd.Id,
            Price = cmd.Price,
            ProductName = cmd.ProductName,
            Quantity = cmd.Quantity
        }))
                   .ReturnsAsync(false);

        _mapper.Setup(c => c.Map<ProductEntity>(cmd))
               .Returns(new ProductEntity
               {
                   Id = cmd.Id,
                   Price = cmd.Price,
                   ProductName = cmd.ProductName,
                   Quantity = cmd.Quantity
               });


        Assert.ThrowsAsync<ArgumentException>(async () => await _handler.Handle(cmd, default));
    }
}
