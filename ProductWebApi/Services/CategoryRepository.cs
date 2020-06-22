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
            return _productContext.Categories.Where(c => c.CategoryId == CategoryId).FirstOrDefault();
        }

        /*public ICollection<Category> GetProductsByCategoryName(string categoryName)
        {
            var id = _productContext.Categories.Where(c => c.categoryName == categoryName)
        }*/

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
