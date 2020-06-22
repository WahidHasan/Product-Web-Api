using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Services
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetProducts();
        Product GetProduct(int ProductId);
        //Task<ICollection<Product>> GetProducts();

        Product GetProductByName(string productName);
        Task<bool> CreateProduct(Product product);
        Task<bool> DeleteProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> Save();
    }
}
