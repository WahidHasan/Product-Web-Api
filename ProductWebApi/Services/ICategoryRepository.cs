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

        Task<ICollection<Product>> GetProductsByCategoryId(int CategoryId);
        Task<bool> CreateCategory(Category category);
        Task<bool> DeleteCategory(Category category);
        Task<bool> UpdateCategory(Category category);
        Task<bool> CategoryIdExists(Product product);
        //Task<bool> CategoryNameExists(Product product);
        Task<bool> Save();
    }
}
