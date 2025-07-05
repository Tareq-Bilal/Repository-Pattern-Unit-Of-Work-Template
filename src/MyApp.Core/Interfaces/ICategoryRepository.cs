using MyApp.Core.Entities;

namespace MyApp.Core.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<IEnumerable<Category>> GetCategoriesWithProductsAsync();
    Task<Category?> GetCategoryWithProductsAsync(int id);
}

