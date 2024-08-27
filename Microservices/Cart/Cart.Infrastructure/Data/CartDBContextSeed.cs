
using Cart.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CartEntity = Cart.Core.Entities.Cart;

namespace Cart.Infrastructure.Data;

public static class CartDBContextSeed
{
    public static async Task SeedAsync(CartDBContext dbContext, ILogger<CartDBContext> logger)
    {

        if (!(await dbContext.Carts.AnyAsync()))
        {
            dbContext.Carts.AddRange(GetCarts());

            await dbContext.SaveChangesAsync();

            logger.LogInformation($"Cart table's seeded");
        }

        if (!(await dbContext.ListItems.AnyAsync()))
        {
            dbContext.ListItems.AddRange(GetListItems());

            await dbContext.SaveChangesAsync();

            logger.LogInformation($"ListItem table's seeded");
        }
    }

    public static IEnumerable<CartEntity> GetCarts()
    {
        return new List<CartEntity>()
        {
            new CartEntity
            {
                Id = new Guid("a6904b79-28ac-45b8-9e85-5ec2a8e7994f"),
                TotalPrice = 10000000,
            }
        };
    }

    public static IEnumerable<ListItem> GetListItems()
    {
        return new List<ListItem>()
        {
            new ListItem
            {
                Id = new Guid("ff47a204-c6a3-4425-bdec-f9d403a65664"),
                ProductId = "2e53f189-129e-417e-86ca-da9a229acce4",
                CartId = new Guid("a6904b79-28ac-45b8-9e85-5ec2a8e7994f"),
                Amount = 2
            },
            new ListItem
            {
                Id = new Guid("4be12645-550e-44cc-a68c-e1ed018c56e2"),
                ProductId = "430d9a98-4032-4fb6-8150-69427baea919",
                CartId = new Guid("a6904b79-28ac-45b8-9e85-5ec2a8e7994f"),
                Amount = 5
            }
        };
    }
}
