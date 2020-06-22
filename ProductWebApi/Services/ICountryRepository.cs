using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Services
{
    public interface ICountryRepository
    {
        Task<ICollection<Country>> GetCountries();
        Country GetCountry(int CountryId);

        //ICollection<Country> GetProductsByCountryId(int CountryId);
        Task<bool> CreateCountry(Country country);
        Task<bool> DeleteCountry(Country country);
        Task<bool> UpdateCountry(Country country);
        Task<bool> Save();
    }
}
