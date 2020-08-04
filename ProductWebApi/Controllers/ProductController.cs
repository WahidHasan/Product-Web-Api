using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebApi.Dtos;
using ProductWebApi.Models;
using ProductWebApi.Services;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductDbContext _productDbContext;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private ICountryRepository _countryRepository;

        public ProductController(ProductDbContext productDbContext,
            IProductRepository productRepository, ICategoryRepository categoryRepository, 
            ICountryRepository countryRepository)
        {
            _productDbContext = productDbContext;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _countryRepository = countryRepository;
            
        }

        [HttpGet("{Id}", Name ="GetProduct")]
        public async Task <IActionResult> GetProduct(int Id)
        {
            try
            {
                if(! await _productRepository.IsProductIdExists(Id))
                {
                    return NotFound();
                }

                var product = await _productRepository.Get(Id);
                var category = _categoryRepository.Get((int)product.CategoryId);
                var country = _countryRepository.Get((int)product.CountryId);
                var productDto = new ProductDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    CategoryId = category.Id,
                    CountryId = country.Id
                };
                return Ok( productDto);
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }

        [HttpGet("getByName/{Name}")]
        public IActionResult GetProductByName(string Name)
        {
            try
            {
                return Ok(_productRepository.GetProductByName(Name));
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            try
            {
                var products = await _productRepository.Gets();
                var productsDto = new List<ProductDto>();
                foreach(var product in products)
                {
                    var category = _categoryRepository.Get((int)product.CategoryId);
                    var country = _countryRepository.Get((int)product.CountryId);
                    productsDto.Add(new ProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        CategoryId = category.Id,
                        CountryId = country.Id
                    });
                }
               

                return Ok(productsDto);
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product toCreate)
        {
            try
            { 
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var categoryname = toCreate.CategoryName;
                var tempCategoryId = await _categoryRepository.GetCategoryIdWithName(toCreate.CategoryName);
                var tempCountryId = await _countryRepository.GetCountryIdWithName(toCreate.CountryName);

                toCreate.CategoryId = tempCategoryId;
                toCreate.CountryId = tempCountryId;

                //Product storeProduct = new Product();
                await _productRepository.Create(toCreate);
        
                return CreatedAtRoute("GetProduct", new Product{ Id = toCreate.Id }, toCreate);
                //return CreatedAtRoute("GetProduct",  toCreate, toCreate);
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }

        //Delete Product
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            //var products = await _productDbContext.Products.Where(p => p.Id == Id).FirstOrDefaultAsync();
            var products = await _productDbContext.Products.FindAsync(Id);
            //var products = _productRepository.Get(Id);
            if (products != null)
            {
                await _productRepository.Delete(products);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }


        //Update Product
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody]Product toUpdate)
        {
            if (toUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (Id != toUpdate.Id)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var check = await _productDbContext.Products.FirstOrDefaultAsync(p => p.Id.Equals(Id));
            check.Id = toUpdate.Id;
            check.Name = toUpdate.Name;
            check.Price = toUpdate.Price;
            check.CountryId = toUpdate.CountryId;
            check.CategoryId = toUpdate.CategoryId;
            await _productRepository.Update(check);

            /*if (!await _productRepository.Update(check))
            {
                ModelState.AddModelError("", $"Something went wrong updating data");
                return StatusCode(500, ModelState);
            } */

            return NoContent();
        }
    }
}