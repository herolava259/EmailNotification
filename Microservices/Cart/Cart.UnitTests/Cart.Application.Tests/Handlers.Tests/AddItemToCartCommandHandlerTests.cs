
using Cart.Application.Commands;
using Cart.Application.Handlers;
using Cart.Core.Repositories;
using Moq;
using CartEntity = Cart.Core.Entities.Cart;
using System.Linq.Expressions;
using Cart.Core.Entities;

namespace Cart.UnitTests.Cart.Application.Tests.Handlers.Tests;

[TestFixture]
public class AddItemToCartCommandHandlerTests
{
    private AddItemToCartCommandHandler _handler;
    private Mock<IListItemRepository> _listItemRepositoryMock;
    private Mock<ICartRepository> _cartRepositoryMock;


    [SetUp]
    public void SetUp()
    {
        _listItemRepositoryMock = new Mock<IListItemRepository>();
        _cartRepositoryMock = new Mock<ICartRepository>();

        _handler = new AddItemToCartCommandHandler(_listItemRepositoryMock.Object,
                                                   _cartRepositoryMock.Object);

    }

    [Test]
    [TestCase("d4717d44-be8d-462f-af1f-a16e437206e3", "f6fdfaba-da68-491d-a559-07f11c3ff1e2",
               12, 1200, 3600, 2)]
    public async Task Handle_Should_Return_True_When_Input_Is_Valid
            (string productId,string cartId, int amount, decimal price, decimal totalPriceInCart, int numItemInCart)
    {
        var cmd = new AddItemToCartCommand(productId, new Guid(cartId), amount, price);


        var currCart = new CartEntity
        {
            Id = new Guid(cartId),
            TotalPrice = totalPriceInCart,

            ListItems = new List<ListItem>
                          {
                              new ListItem
                              {
                                  Id = Guid.NewGuid(),
                                  ProductId = productId,
                                  Amount = numItemInCart,
                                  CartId = new Guid(cartId)
                              },

                              new ListItem
                              {
                                  Id = Guid.NewGuid(),
                                  ProductId = "736fd89c-c883-4aa8-a55e-a2e246fecf45",
                                  Amount = 1,
                                  CartId = new Guid(cartId)
                              }
                          }
        };
        _cartRepositoryMock.Setup(c => c.GetAsync(It.IsAny<Expression<Func<CartEntity, bool>>>(),
                              It.IsAny<bool>(),It.IsAny<string>()))
                      .Returns(Task.FromResult<CartEntity?>(currCart));

        _cartRepositoryMock.Setup(c => c.PartialUpdateAsync(It.IsAny<CartEntity>(), It.IsAny<List<string>>()))
                       .Returns(Task.FromResult(true));

        _listItemRepositoryMock.Setup(c => c.UpdateAsync(It.IsAny<ListItem>()))
                           .Returns(Task.FromResult(true));

        _listItemRepositoryMock.Setup(c => c.CreateAsync(It.IsAny<ListItem>()))
                           .Returns(Task.FromResult(Guid.NewGuid()));


        var result = await _handler.Handle(cmd, default);

        Assert.True(result);

        _cartRepositoryMock.Verify(c => c.PartialUpdateAsync(It.Is<CartEntity>(c => c.Id == new Guid(cartId)
                                                                             && c.TotalPrice == totalPriceInCart + amount * price),
                                                         It.IsAny<List<string>>())
                              , Times.Once);

        _listItemRepositoryMock.Verify(c => c.UpdateAsync(It.Is<ListItem>(c => c.ProductId == productId &&
                                                                           c.CartId == new Guid(cartId) &&
                                                                           c.Amount == amount + numItemInCart
                                                                     )
                                                      )
                                  ,Times.Once);


        
    }

    [Test]
    [TestCase("d4717d44-be8d-462f-af1f-a16e437206e3", "f6fdfaba-da68-491d-a559-07f11c3ff1e2",
               12, 1200, 3600, 0)]
    public async Task Handle_Should_Return_True_When_Input_Is_Valid_And_Newly_Item_Selected
            (string productId, string cartId, int amount, decimal price, decimal totalPriceInCart, int numItemInCart)
    {
        var cmd = new AddItemToCartCommand(productId, new Guid(cartId), amount, price);


        var currCart = new CartEntity
        {
            Id = new Guid(cartId),
            TotalPrice = totalPriceInCart,

            ListItems = new List<ListItem>
                          {

                              new ListItem
                              {
                                  Id = Guid.NewGuid(),
                                  ProductId = "736fd89c-c883-4aa8-a55e-a2e246fecf45",
                                  Amount = 1,
                                  CartId = new Guid(cartId)
                              }
                          }
        };
        _cartRepositoryMock.Setup(c => c.GetAsync(It.IsAny<Expression<Func<CartEntity, bool>>>(),
                              It.IsAny<bool>(), It.IsAny<string>()))
                      .Returns(Task.FromResult<CartEntity?>(currCart));

        _cartRepositoryMock.Setup(c => c.PartialUpdateAsync(It.IsAny<CartEntity>(), It.IsAny<List<string>>()))
                       .Returns(Task.FromResult(true));

        _listItemRepositoryMock.Setup(c => c.UpdateAsync(It.IsAny<ListItem>()))
                           .Returns(Task.FromResult(true));

        _listItemRepositoryMock.Setup(c => c.CreateAsync(It.IsAny<ListItem>()))
                           .Returns(Task.FromResult(Guid.NewGuid()));


        var result = await _handler.Handle(cmd, default);

        Assert.True(result);

        _cartRepositoryMock.Verify(c => c.PartialUpdateAsync(It.Is<CartEntity>(c => c.Id == new Guid(cartId)
                                                                             && c.TotalPrice == totalPriceInCart + amount * price),
                                                         It.IsAny<List<string>>())
                              , Times.Once);

        _listItemRepositoryMock.Verify(c => c.CreateAsync(It.Is<ListItem>(c => c.ProductId == productId &&
                                                                           c.CartId == new Guid(cartId) &&
                                                                           c.Amount == amount + numItemInCart
                                                                     )
                                                      )
                                  , Times.Once);

        
    }

    [Test]
    [TestCase("d4717d44-be8d-462f-af1f-a16e437206e3", "f6fdfaba-da68-491d-a559-07f11c3ff1e2",
               12, 1200, 3600, 0)]
    public async Task Handle_Repository_Should_Add_Newly_Item_When_Newly_Item_Selected
            (string productId, string cartId, int amount, decimal price, decimal totalPriceInCart, int numItemInCart)
    {
        var cmd = new AddItemToCartCommand(productId, new Guid(cartId), amount, price);


        var currCart = new CartEntity
        {
            Id = new Guid(cartId),
            TotalPrice = totalPriceInCart,

            ListItems = new List<ListItem>
                          {

                              new ListItem
                              {
                                  Id = Guid.NewGuid(),
                                  ProductId = "736fd89c-c883-4aa8-a55e-a2e246fecf45",
                                  Amount = 1,
                                  CartId = new Guid(cartId)
                              }
                          }
        };
        _cartRepositoryMock.Setup(c => c.GetAsync(It.IsAny<Expression<Func<CartEntity, bool>>>(),
                              It.IsAny<bool>(), It.IsAny<string>()))
                      .Returns(Task.FromResult<CartEntity?>(currCart));

        _cartRepositoryMock.Setup(c => c.PartialUpdateAsync(It.IsAny<CartEntity>(), It.IsAny<List<string>>()))
                       .Returns(Task.FromResult(true));

        _listItemRepositoryMock.Setup(c => c.UpdateAsync(It.IsAny<ListItem>()))
                           .Returns(Task.FromResult(true));

        _listItemRepositoryMock.Setup(c => c.CreateAsync(It.IsAny<ListItem>()))
                           .Returns(Task.FromResult(Guid.NewGuid()));


        _ = await _handler.Handle(cmd, default);

        _listItemRepositoryMock.Verify(c => c.CreateAsync(It.Is<ListItem>(c => c.ProductId == productId)),
                                   Times.Once);
    }


    [Test]
    [TestCase("d4717d44-be8d-462f-af1f-a16e437206e3", "f6fdfaba-da68-491d-a559-07f11c3ff1e2",
               12, 1200, 3600)]
    public async Task Handle_Should_Return_False_When_Cart_Is_Not_Existed
            (string productId, string cartId, int amount, decimal price, decimal totalPriceInCart)
    {
        var cmd = new AddItemToCartCommand(productId, new Guid(cartId), amount, price);

        _cartRepositoryMock.Setup(c => c.GetAsync(It.IsAny<Expression<Func<CartEntity, bool>>>(),
                              It.IsAny<bool>(), It.IsAny<string>()))
                      .Returns(Task.FromResult<CartEntity?>(null));

        _cartRepositoryMock.Setup(c => c.PartialUpdateAsync(It.IsAny<CartEntity>(), It.IsAny<List<string>>()))
                       .Returns(Task.FromResult(true));

        _listItemRepositoryMock.Setup(c => c.UpdateAsync(It.IsAny<ListItem>()))
                           .Returns(Task.FromResult(true));

        _listItemRepositoryMock.Setup(c => c.CreateAsync(It.IsAny<ListItem>()))
                           .Returns(Task.FromResult(Guid.NewGuid()));


        var result = await _handler.Handle(cmd, default);

        Assert.False(result);
    }

    [Test]
    [TestCase("d4717d44-be8d-462f-af1f-a16e437206e3", "f6fdfaba-da68-491d-a559-07f11c3ff1e2",
               12, 1200, 3600, 2)]
    public async Task Handle_Should_Return_False_When_Repository_Not_Update_ListItem
            (string productId, string cartId, int amount, decimal price, decimal totalPriceInCart, int numItemInCart)
    {
        var cmd = new AddItemToCartCommand(productId, new Guid(cartId), amount, price);


        var currCart = new CartEntity
        {
            Id = new Guid(cartId),
            TotalPrice = totalPriceInCart,

            ListItems = new List<ListItem>
                          {
                              new ListItem
                              {
                                  Id = Guid.NewGuid(),
                                  ProductId = productId,
                                  Amount = numItemInCart,
                                  CartId = new Guid(cartId)
                              },

                              new ListItem
                              {
                                  Id = Guid.NewGuid(),
                                  ProductId = "736fd89c-c883-4aa8-a55e-a2e246fecf45",
                                  Amount = 1,
                                  CartId = new Guid(cartId)
                              }
                          }
        };
        _cartRepositoryMock.Setup(c => c.GetAsync(It.IsAny<Expression<Func<CartEntity, bool>>>(),
                              It.IsAny<bool>(), It.IsAny<string>()))
                      .Returns(Task.FromResult<CartEntity?>(currCart));

        _cartRepositoryMock.Setup(c => c.PartialUpdateAsync(It.IsAny<CartEntity>(), It.IsAny<List<string>>()))
                       .Returns(Task.FromResult(true));

        _listItemRepositoryMock.Setup(c => c.UpdateAsync(It.IsAny<ListItem>()))
                           .Returns(Task.FromResult(false));

        _listItemRepositoryMock.Setup(c => c.CreateAsync(It.IsAny<ListItem>()))
                           .Returns(Task.FromResult(Guid.NewGuid()));


        var result = await _handler.Handle(cmd, default);

        Assert.False(result);

        
    }

    [Test]
    [TestCase("d4717d44-be8d-462f-af1f-a16e437206e3", "f6fdfaba-da68-491d-a559-07f11c3ff1e2",
               12, 1200, 3600, 2)]
    public async Task Handle_Should_Return_False_When_Repository_Not_Create_ListItem_If_New_Item_Inserted
            (string productId, string cartId, int amount, decimal price, decimal totalPriceInCart, int numItemInCart)
    {
        var cmd = new AddItemToCartCommand(productId, new Guid(cartId), amount, price);


        var currCart = new CartEntity
        {
            Id = new Guid(cartId),
            TotalPrice = totalPriceInCart,

            ListItems = new List<ListItem>
                          {

                              new ListItem
                              {
                                  Id = Guid.NewGuid(),
                                  ProductId = "736fd89c-c883-4aa8-a55e-a2e246fecf45",
                                  Amount = 1,
                                  CartId = new Guid(cartId)
                              }
                          }
        };
        _cartRepositoryMock.Setup(c => c.GetAsync(It.IsAny<Expression<Func<CartEntity, bool>>>(),
                              It.IsAny<bool>(), It.IsAny<string>()))
                      .Returns(Task.FromResult<CartEntity?>(currCart));

        _cartRepositoryMock.Setup(c => c.PartialUpdateAsync(It.IsAny<CartEntity>(), It.IsAny<List<string>>()))
                       .Returns(Task.FromResult(true));

        _listItemRepositoryMock.Setup(c => c.UpdateAsync(It.IsAny<ListItem>()))
                           .Returns(Task.FromResult(false));

        _listItemRepositoryMock.Setup(c => c.CreateAsync(It.IsAny<ListItem>()))
                           .Returns(Task.FromResult(Guid.Empty));


        var result = await _handler.Handle(cmd, default);

        Assert.False(result);

        
    }

    [Test]
    [TestCase("d4717d44-be8d-462f-af1f-a16e437206e3", "f6fdfaba-da68-491d-a559-07f11c3ff1e2",
               12, 1200, 3600, 2)]
    public async Task Handle_Should_Return_False_When_Repository_Not_Partial_Update_Product
            (string productId, string cartId, int amount, decimal price, decimal totalPriceInCart, int numItemInCart)
    {
        var cmd = new AddItemToCartCommand(productId, new Guid(cartId), amount, price);


        var currCart = new CartEntity
        {
            Id = new Guid(cartId),
            TotalPrice = totalPriceInCart,

            ListItems = new List<ListItem>
                          {
                              new ListItem
                              {
                                  Id = Guid.NewGuid(),
                                  ProductId = productId,
                                  Amount = numItemInCart,
                                  CartId = new Guid(cartId)
                              },

                              new ListItem
                              {
                                  Id = Guid.NewGuid(),
                                  ProductId = "736fd89c-c883-4aa8-a55e-a2e246fecf45",
                                  Amount = 1,
                                  CartId = new Guid(cartId)
                              }
                          }
        };
        _cartRepositoryMock.Setup(c => c.GetAsync(It.IsAny<Expression<Func<CartEntity, bool>>>(),
                              It.IsAny<bool>(), It.IsAny<string>()))
                      .Returns(Task.FromResult<CartEntity?>(currCart));

        _cartRepositoryMock.Setup(c => c.PartialUpdateAsync(It.IsAny<CartEntity>(), It.IsAny<List<string>>()))
                       .Returns(Task.FromResult(false));

        _listItemRepositoryMock.Setup(c => c.UpdateAsync(It.IsAny<ListItem>()))
                           .Returns(Task.FromResult(true));

        _listItemRepositoryMock.Setup(c => c.CreateAsync(It.IsAny<ListItem>()))
                           .Returns(Task.FromResult(Guid.NewGuid()));


        var result = await _handler.Handle(cmd, default);

        Assert.False(result);


    }
}
