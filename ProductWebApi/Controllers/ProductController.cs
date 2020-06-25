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
        private ICheckService _checkService;

        public ProductController(ProductDbContext productDbContext,
            IProductRepository productRepository, ICategoryRepository categoryRepository, 
            ICountryRepository countryRepository, ICheckService checkService)
        {
            _productDbContext = productDbContext;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _countryRepository = countryRepository;
            _checkService = checkService;
        }

        [HttpGet("{Id}", Name ="GetProduct")]
        public async Task <IActionResult> GetProduct(int Id)
        {
            try
            {
                if(! await _productRepository.ProductIdExists(Id))
                {
                    return NotFound();
                }

                var product = await _productRepository.GetProduct(Id);
                var category = _categoryRepository.GetCategory((int)product.CategoryId);
                var country = _countryRepository.GetCountry((int)product.CountryId);
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
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _productRepository.GetProducts();
                var productsDto = new List<ProductDto>();
                foreach(var product in products)
                {
                    var category = _categoryRepository.GetCategory((int)product.CategoryId);
                    var country = _countryRepository.GetCountry((int)product.CountryId);
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
        public async Task<IActionResult> CreateProduct([FromBody] Product toCreate)
        {
           /* try
            { */
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                var tempCategoryId = await _checkService.CategoryIdFetchByName(toCreate);
                var tempCountryId = await _checkService.CountryIdFetchByName(toCreate);

                toCreate.CategoryId = tempCategoryId;
                toCreate.CountryId = tempCountryId;

                //Product storeProduct = new Product();
                bool X =await _productRepository.CreateProduct(toCreate);
                if (X == true)
                    return CreatedAtRoute("GetProduct", new Product { Id = toCreate.Id }, toCreate);
                else
                    return NotFound();

                //return CreatedAtRoute("GetProduct", new Product{ Id = toCreate.Id }, toCreate);
                //return CreatedAtRoute("GetProduct",  toCreate, toCreate);
           // }
            /*catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }*/
        }


        [HttpDelete("{Id}")]

        public async Task<IActionResult> DeleteProduct([FromRoute] int ProductId)
        {
            //var products = await _productDbContext.Products.Where(p => p.Id == Id).FirstOrDefaultAsync();
            var products = await _productDbContext.Products.FindAsync(ProductId);
            //var products = _productRepository.GetProduct(Id);
            if (products != null)
            {
                await _productRepository.DeleteProduct(products);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }



        [HttpPut("{Id}")]

        public async Task<IActionResult> UpdateProduct(int ProductId, [FromBody]Product toUpdate)
        {
            if (toUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (ProductId != toUpdate.Id)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var check = await _productDbContext.Products.FirstOrDefaultAsync(p => p.Id.Equals(ProductId));
            check.Id = toUpdate.Id;
            check.Name = toUpdate.Name;
            check.Price = toUpdate.Price;
            check.CountryId = toUpdate.CountryId;
            check.CategoryId = toUpdate.CategoryId;
            await _productRepository.UpdateProduct(check);

            /*if (!await _productRepository.UpdateProduct(check))
            {
                ModelState.AddModelError("", $"Something went wrong updating data");
                return StatusCode(500, ModelState);
            } */

            return NoContent();
        }
    }
}