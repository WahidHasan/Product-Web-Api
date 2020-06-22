﻿using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> CreateCountry(Country country)
        {
            _productContext.Add(country);
            return await Save();
        }

        public async Task<bool> DeleteCountry(Country country)
        {
            _productContext.Remove(country);
            return await Save();
        }

        public async Task<ICollection<Country>> GetCountries()
        {
            return await _productContext.Countries.ToListAsync();
        }

        public Country GetCountry(int CountryId)
        {
            return _productContext.Countries.Where(c => c.CountryId == CountryId).FirstOrDefault();
        }

        public async Task<bool> Save()
        {
            var saved = await _productContext.SaveChangesAsync();
            return saved >= 0 ? true : false;
        }

        public async Task<bool> UpdateCountry(Country country)
        {
            _productContext.Update(country);
            return await Save();
        }
    }
}