using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Product.Core.Repositories;
using System.Xml.Linq;
using ProductEntity = Product.Core.Entities.Product;

namespace Product.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task<bool> CreateProduct(Core.Entities.Product entity)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            
            var affectedRow = await
                                connection.ExecuteAsync
                                ("INSERT INTO Product (ProductName, Price, Quantity) VALUES (@ProductName, @Price, @Quantity)",
                                    new
                                    {
                                        ProductName = entity.ProductName,
                                        Price = entity.Price,
                                        Quantity = entity.Quantity,
                                        
                                    });

            if (affectedRow == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affectedRow = await connection.ExecuteAsync("DELETE FROM Product WHERE Id = @Id",
                                            new { Id = id });

            if (affectedRow == 0) return false;

            return true;
        }

        public async Task<List<ProductEntity>> GetAllProduct()
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var reader = await connection.QueryMultipleAsync
                ("SELECT * FROM Product");

            if (reader is null)
                return new();


            return reader.Read<ProductEntity>().ToList();
        }

        public async Task<ProductEntity?> GetProductById(Guid id)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var product = await connection.QueryFirstOrDefaultAsync<ProductEntity>
                ("SELECT * FROM Product WHERE Id = @Id", new { Id = id });

            if (product is null)
                return default;

            return product;
        }

        public async Task<ProductEntity?> GetProductByName(string name)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var product = await connection.QueryFirstOrDefaultAsync<ProductEntity>
                ("SELECT * FROM Product WHERE ProductName = @ProductName", new { ProductName = name });

            if (product is null)
                return default;

            return product;
        }

        public async Task<bool> UpdateProduct(ProductEntity entity)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affectedRow = await
                                connection.ExecuteAsync
                                ("UPDATE Product SET ProductName=@ProductName, Price = @Price, Quantity = @Quantity WHERE Id = @Id",
                                    new 
                                    { 
                                        Id = entity.Id,
                                        ProductName = entity.ProductName,
                                        Price = entity.Price,
                                        Quantity = entity.Quantity, 
                                        
                                    });

            if (affectedRow == 0)
                return false;

            return true;
        }
    }
}
