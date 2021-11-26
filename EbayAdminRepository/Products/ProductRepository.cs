namespace EbayAdminRepository.Products
{
    using EbayAdminModels.Products;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProductRepository : IProductRepository
    {
        private readonly Ecc _ecc;
        public ProductRepository(Ecc ecc)
        {
            _ecc = ecc;
        }
        public async Task<int> AddProductAsync(Product product)
        {
            await _ecc.AddAsync(product);
            await _ecc.SaveChangesAsync();
            return product.Id;
        }
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _ecc.Products.ToList();
        }
    }
}
