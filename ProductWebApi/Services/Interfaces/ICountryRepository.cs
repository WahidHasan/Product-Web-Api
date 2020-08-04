using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Services
{
    public interface ICountryRepository
    {
        Task<ICollection<Country>> Gets();
        Country Get(int Id);

        Task<ICollection<Product>> GetProductsByCountryId(int Id);
        Task<bool> Create(Country country);
        Task<bool> Delete(Country country);
        Task<bool> Update(Country country);
        Task<bool> IsCountryIdExists(int Id);
        Task<int> GetCountryIdWithName(string countryName);
        Task<bool> Save();
    }
}
