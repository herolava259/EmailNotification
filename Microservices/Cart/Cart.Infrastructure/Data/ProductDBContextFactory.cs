using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Cart.Infrastructure.Data;

public class ProductDBContextFactory: IDesignTimeDbContextFactory<ProductDBContext>
{
    public ProductDBContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<ProductDBContext>();

        optionBuilder.UseSqlServer("Data Source=ProductDb");

        return new ProductDBContext(optionBuilder.Options);
    }
}
