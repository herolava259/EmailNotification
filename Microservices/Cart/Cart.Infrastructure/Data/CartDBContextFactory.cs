using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Cart.Infrastructure.Data;

public class CartDBContextFactory: IDesignTimeDbContextFactory<CartDBContext>
{
    public CartDBContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<CartDBContext>();

        optionBuilder.UseSqlServer("Data Source=ProductDb");

        return new CartDBContext(optionBuilder.Options);
    }
}
