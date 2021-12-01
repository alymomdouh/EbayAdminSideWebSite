namespace EbayAdminModels.Category
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryRepository
    {
        Task<int> AddCategoryAsync(Category category);
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryDetailsAsync(int value);
        Task<int> UpdateCategoryAsync(Category product);
        Task<int> DeleteCategoryAsync(Category product);
    }
}
