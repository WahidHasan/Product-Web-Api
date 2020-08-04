using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Services
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> Gets();
        Category Get(int CategoryId);
        //Task<ICollection<Product>> Gets();

        Task<ICollection<Product>> GetProductsByCategoryId(int CategoryId);
        Task<bool> Create(Category category);
        Task<bool> Delete(Category category);
        Task<bool> Update(Category category);
        Task<bool> IsCategoryIdExists(int Id);
        Task<int> GetCategoryIdWithName(string categoryName);
        //Task<bool> CategoryNameExists(Product product);
        Task<bool> Save();
    }
}
