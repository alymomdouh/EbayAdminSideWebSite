namespace EbayAdminRepository.Products
{
    using EbayAdminDbContext;
    using EbayAdminModels.Pagination;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProductRepository : IProductRepository
    {
        private readonly myDbContext _context;
        public ProductRepository(myDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddProductAsync(Product product)
        {
            await _context.AddAsync(product);
            try
            {
                await _context.SaveChangesAsync();

            }
            catch(Exception )
            {

            }
            return product.ProductId;
        }

        public async Task<int> DeleteProductAsync(Product product)
        {
            _context.Remove(product);
           await _context.SaveChangesAsync();
            return product.ProductId;
        }

        public async Task<int> GetProductCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<Product> GetProductDetailsAsync(int value)
        {
            return await _context.Products
                                .Include(p=>  p.Admin )
                                .Include(p=>  p.brands) // add by aly
                                .Include(p=>  p.category)
                                .Include(p=>  p.subCategory)
                                .Include(p=>  p.stock)
                                .Include(p=>  p.rates)
                                .Include(p=>  p.comments)
                                .Include(p=>  p.productImgs)
                                .Where(p => p.ProductId == value)
                                .FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<PaginationResult> GetProductPagesAsync(PaginationSearch pagination)
        {
            var paginationResult = new PaginationResult();

            paginationResult.RowCount = await _context.Products.CountAsync();
            paginationResult.PageCount = (int)Math.Ceiling((double)paginationResult.RowCount / pagination.PageSize);
            paginationResult.PageSize = pagination.PageSize;
            paginationResult.PagNumber = pagination.PagNumber;

            paginationResult.Data = await _context.Products
                .Skip((pagination.PagNumber-1) * pagination.PageSize).
                Take(pagination.PageSize).ToListAsync();

            return paginationResult;
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return product.ProductId;
        }
    }
}
