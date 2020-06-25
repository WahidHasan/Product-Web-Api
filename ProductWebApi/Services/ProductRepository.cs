using Microsoft.EntityFrameworkCore;
using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _productContext;

        public ProductRepository(ProductDbContext productContext)
        {
            _productContext = productContext;
        }
        public async Task<bool> CreateProduct(Product product)
        {
            _productContext.Add(product);
            return await Save();
        }

        public async Task<bool> DeleteProduct(Product product)
        {
            _productContext.Remove(product);
            return await Save();
        }

        public async Task<ICollection<Product>> GetProducts()
        {
            return await _productContext.Products.ToListAsync();
        }

        public async Task<Product> GetProduct(int ProductId)
        {
           return await _productContext.Products.Where(p => p.Id == ProductId).FirstOrDefaultAsync();
        }

        public Product GetProductByName(string productName)
        {
            return _productContext.Products.Where(p => p.Name == productName).FirstOrDefault();
        }

        public async Task<bool> Save()
        {
            var saved = await _productContext.SaveChangesAsync();
            return saved >= 0 ? true : false;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            _productContext.Update(product);
            return await Save();
        }

        public async Task<bool> ProductIdExists(int ProductId)
        {
            return await _productContext.Products.AnyAsync(c => c.Id == ProductId);
        }
    }
}
