using Microsoft.EntityFrameworkCore;
using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Services
{
    public class CheckService : ICheckService
    {
        private readonly ProductDbContext _productDbContext;
        private ICategoryRepository _categoryRepository;
        private ICountryRepository _countryRepository;
        private IProductRepository _productRepository;

        public CheckService(ProductDbContext productDbContext, 
            IProductRepository productRepository, ICategoryRepository categoryRepository,
            ICountryRepository countryRepository)
        {
            _productDbContext = productDbContext;
            _categoryRepository = categoryRepository;
            _countryRepository = countryRepository;
            _productRepository = productRepository;
        }

        public async Task<int> CategoryIdFetchByName(Product product)
        {
            var categoryId = await _productDbContext.Categories.Where(c => c.Name == product.CategoryName)
                .Select(c => c.Id).FirstOrDefaultAsync();
            //var categoryId =  _productDbContext.Categories.FirstOrDefaultAsync(c => c.Name == product.CategoryName);
            if (categoryId == default)
            {
                var demoCategory = new Category();
                demoCategory.Name = product.CategoryName;
                await _categoryRepository.CreateCategory(demoCategory);
                return demoCategory.Id;
            }

            return categoryId;
        }

        public async Task<int> CountryIdFetchByName(Product product)
        {
            var countryId =await _productDbContext.Countries.Where(c => c.Name == product.CountryName)
                .Select(c => c.Id).FirstOrDefaultAsync();

            if (countryId == default)
            {
                var demoCountry = new Country();
                demoCountry.Name = product.CountryName;
                await _countryRepository.CreateCountry(demoCountry);
                return demoCountry.Id;
            }

            return countryId;
        }
    }
}
