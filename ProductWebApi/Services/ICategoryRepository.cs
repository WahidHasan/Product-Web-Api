using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Services
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetCategories();
        Category GetCategory(int CategoryId);
        //Task<ICollection<Product>> GetCategories();

        //ICollection<Category> GetProductsByCategoryName(string categoryName);
        Task<bool> CreateCategory(Category category);
        Task<bool> DeleteCategory(Category category);
        Task<bool> UpdateCategory(Category category);
        Task<bool> Save();
    }
}
