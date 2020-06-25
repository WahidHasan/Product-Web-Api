using Microsoft.EntityFrameworkCore;
using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductDbContext _productContext;

        public CategoryRepository(ProductDbContext productContext)
        {
            _productContext = productContext;
        }

        public async Task<bool> CategoryIdExists(Product product)
        {
            return await _productContext.Categories.AnyAsync(c => c.Id == product.CategoryId);
        }

        /*public async Task<bool> CategoryNameExists(Product product)
        {
            return await _productContext.Categories.AnyAsync(c => c.Id == product.Id);
        }*/

        public async Task<bool> CreateCategory(Category category)
        {
            _productContext.Add(category);
            return await Save();
        }

        public async Task<bool> DeleteCategory(Category category)
        {
            _productContext.Remove(category);
            return await Save();
        }

        public async Task<ICollection<Category>> GetCategories()
        {
            return await _productContext.Categories.ToListAsync();
        }

        public Category GetCategory(int CategoryId)
        {
            return _productContext.Categories.Where(c => c.Id == CategoryId).FirstOrDefault();
        }

        public async Task<ICollection<Product>> GetProductsByCategoryId(int CategoryId)
        {
            return await _productContext.Products.Where(c => c.CategoryId == CategoryId).ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _productContext.SaveChangesAsync();
            return saved >= 0 ? true : false;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            _productContext.Update(category);
            return await Save();
        }
    }
}
