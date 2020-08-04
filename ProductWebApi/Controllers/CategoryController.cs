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
        [HttpGet("{Id}", Name = "GetCategory")]
        public async Task<IActionResult> GetCategory(int Id)
        {
            try
            {
                if (!await _categoryRepository.IsCategoryIdExists(Id))
                {
                    return NotFound();
                }
                var category = _categoryRepository.Get(Id);
                var categoryDto = new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name
                };
                return Ok(categoryDto);
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }

        //Get All Categories
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            try
            {
                var categories = await _categoryRepository.Gets();
                var categoriesDto = new List<CategoryDto>();
                foreach (var category in categories)
                {
                    categoriesDto.Add(new CategoryDto
                    {
                        Id = category.Id,
                        Name = category.Name
                    });
                }
                return Ok(categoriesDto);
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }


        //Create new Category
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category toCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _categoryRepository.Create(toCreate);

                /*if (!await _productRepository.Create(toCreate))
                {
                    ModelState.AddModelError("", $"Something went wrong saving data");
                    return StatusCode(500, ModelState);
                } */
                return CreatedAtRoute("GetCategory", new Category { Id = toCreate.Id }, toCreate);
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }

        //Delete Category
        [HttpDelete("{Id}")]

        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            //var products = await _productDbContext.Products.Where(p => p.Id == Id).FirstOrDefaultAsync();
            var categories = await _productDbContext.Categories.FindAsync(Id);
            //var products = _productRepository.Get(Id);
            if (categories != null)
            {
                await _categoryRepository.Delete(categories);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        //Update Category
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody]Category toUpdate)
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

            var check = await _productDbContext.Categories.FirstOrDefaultAsync(c => c.Id.Equals(Id));
            check.Id = toUpdate.Id;
            check.Name = toUpdate.Name;

            await _categoryRepository.Update(check);

            /*if (!await _productRepository.Update(check))
            {
                ModelState.AddModelError("", $"Something went wrong updating data");
                return StatusCode(500, ModelState);
            } */

            return NoContent();
        }

    }
}