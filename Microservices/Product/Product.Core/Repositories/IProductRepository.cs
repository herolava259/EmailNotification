using ProductEntity = Product.Core.Entities.Product;

namespace Product.Core.Repositories;

public interface IProductRepository
{
    Task<ProductEntity?> GetProductById(Guid id);
    Task<bool> CreateProduct(ProductEntity entity);
    Task<bool> UpdateProduct(ProductEntity entity);

    Task<bool> DeleteProduct(Guid id);

    Task<List<ProductEntity>> GetAllProduct();

    Task<ProductEntity?> GetProductByName(string name);
}
