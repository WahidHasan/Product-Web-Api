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
    public class CategoryController : ControllerBase
    {
        private ProductDbContext _productDbContext;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;

        public CategoryController(ProductDbContext productDbContext,
            ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _productDbContext = productDbContext;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        //Get Category by Id
        [HttpGet("{CategoryId}", Name = "GetCategory")]
        public IActionResult GetCategory(int CategoryId)
        {
            try
            {
                return Ok(_categoryRepository.GetCategory(CategoryId));
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }

        //Get All Categories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                return Ok(await _categoryRepository.GetCategories());
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }

        //Create new Category
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category toCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _categoryRepository.CreateCategory(toCreate);

                /*if (!await _productRepository.CreateProduct(toCreate))
                {
                    ModelState.AddModelError("", $"Something went wrong saving data");
                    return StatusCode(500, ModelState);
                } */
                return CreatedAtRoute("GetCategory", new Category { CategoryId = toCreate.CategoryId }, toCreate);
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }

        //Delete Category
        [HttpDelete("{CategoryId}")]

        public async Task<IActionResult> DeleteCategory([FromRoute] int CategoryId)
        {
            //var products = await _productDbContext.Products.Where(p => p.ProductId == ProductId).FirstOrDefaultAsync();
            var categories = await _productDbContext.Categories.FindAsync(CategoryId);
            //var products = _productRepository.GetProduct(ProductId);
            if (categories != null)
            {
                await _categoryRepository.DeleteCategory(categories);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        //Update Category
        [HttpPut("{CategoryId}")]
        public async Task<IActionResult> UpdateCategory(int CategoryId, [FromBody]Category toUpdate)
        {
            if (toUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (CategoryId != toUpdate.CategoryId)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var check = await _productDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId.Equals(CategoryId));
            check.CategoryId = toUpdate.CategoryId;
            check.categoryName = toUpdate.categoryName;

            await _categoryRepository.UpdateCategory(check);

            /*if (!await _productRepository.UpdateProduct(check))
            {
                ModelState.AddModelError("", $"Something went wrong updating data");
                return StatusCode(500, ModelState);
            } */

            return NoContent();
        }

    }
}