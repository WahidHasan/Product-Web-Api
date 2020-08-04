using Microsoft.EntityFrameworkCore;
using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Services
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ProductDbContext _productContext;

        public CountryRepository(ProductDbContext productContext)
        {
            _productContext = productContext;
        }

        public async Task<bool> IsCountryIdExists(int Id)
        {
            return await _productContext.Countries.AnyAsync(c => c.Id == Id);
        }

        public async Task<bool> Create(Country country)
        {
            _productContext.Add(country);
            return await Save();
        }

        public async Task<bool> Delete(Country country)
        {
            _productContext.Remove(country);
            return await Save();
        }

        public async Task<ICollection<Country>> Gets()
        {
            return await _productContext.Countries.ToListAsync();
        }

        public Country Get(int Id)
        {
            return _productContext.Countries.Where(c => c.Id == Id).FirstOrDefault();
        }

        public async Task<ICollection<Product>> GetProductsByCountryId(int Id)
        {
            return await _productContext.Products.Where(c => c.CountryId == Id).ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _productContext.SaveChangesAsync();
            return saved >= 0 ? true : false;
        }

        public async Task<bool> Update(Country country)
        {
            _productContext.Update(country);
            return await Save();
        }

        public async Task<int> GetCountryIdWithName(string countryName)
        {
            var country = await _productContext.Countries.Where(c => c.Name == countryName).Select(c => c.Id).FirstOrDefaultAsync();
            //var country = await _productContext.Countries.FirstOrDefaultAsync(c => c.Name == countryName);
            if (country == default)
            {
                var demoCountry = new Country();
                demoCountry.Name = countryName;
                await Create(demoCountry);
                return demoCountry.Id;
            }

            //int countryValue = (int)country.Id;
            //return countryValue;
            return country;
        }
    }
}
