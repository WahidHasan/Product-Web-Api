using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public ProductController(ProductDbContext productDbContext,
            IProductRepository productRepository)
        {
            _productDbContext = productDbContext;
            _productRepository = productRepository;
        }

        [HttpGet("{ProductId}", Name ="GetProduct")]
        public IActionResult GetProduct(int ProductId)
        {
            try
            {
                return Ok( _productRepository.GetProduct(ProductId));
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }

        [HttpGet("getByName/{productName}")]
        public IActionResult GetProductByName(string productName)
        {
            try
            {
                return Ok(_productRepository.GetProductByName(productName));
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
                return Ok(await _productRepository.GetProducts());
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product toCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _productRepository.CreateProduct(toCreate);

                /*if (!await _productRepository.CreateProduct(toCreate))
                {
                    ModelState.AddModelError("", $"Something went wrong saving data");
                    return StatusCode(500, ModelState);
                } */
                  return CreatedAtRoute("GetProduct", new { ProductId = toCreate.ProductId }, toCreate);
                //return CreatedAtRoute("GetProduct",  toCreate, toCreate);
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }


        [HttpDelete("{ProductId}")]

        public async Task<IActionResult> DeleteProduct([FromRoute] int ProductId)
        {
            //var products = await _productDbContext.Products.Where(p => p.ProductId == ProductId).FirstOrDefaultAsync();
            var products = await _productDbContext.Products.FindAsync(ProductId);
            //var products = _productRepository.GetProduct(ProductId);
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



        [HttpPut("{ProductId}")]

        public async Task<IActionResult> UpdateProduct(int ProductId, [FromBody]Product toUpdate)
        {
            if (toUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (ProductId != toUpdate.ProductId)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var check = await _productDbContext.Products.FirstOrDefaultAsync(p => p.ProductId.Equals(ProductId));
            check.ProductId = toUpdate.ProductId;
            check.productName = toUpdate.productName;
            check.price = toUpdate.price;
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