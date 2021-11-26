using System.Collections.Generic;
using System.Threading.Tasks;

namespace EbayAdminModels.Products
{
    
    public interface IProductRepository
    {
        Task<int> AddProductAsync(Product product);
        Task<List<Product>> GetProductsAsync();
    }
}
