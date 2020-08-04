using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Services
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> Gets();
        Task<Product> Get(int ProductId);
        //Task<ICollection<Product>> Gets();

        Product GetProductByName(string productName);
        Task<bool> Create(Product product);
        Task<bool> Delete(Product product);
        Task<bool> Update(Product product);
        Task<bool> IsProductIdExists(int ProductId);
        Task<bool> Save();
    }
}
