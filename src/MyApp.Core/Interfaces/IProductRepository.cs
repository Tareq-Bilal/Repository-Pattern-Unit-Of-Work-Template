using MyApp.Core.Entities;

namespace MyApp.Core.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
    Task<IEnumerable<Product>> GetProductsWithCategoryAsync();
    Task<Product?> GetProductWithCategoryAsync(int id);
}

