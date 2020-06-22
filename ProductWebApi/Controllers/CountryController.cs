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
    public class CountryController : ControllerBase
    {
        private ProductDbContext _productDbContext;
        private ICountryRepository _countryRepository;
        private IProductRepository _productRepository;

        public CountryController(ProductDbContext productDbContext,
            ICountryRepository countryRepository, IProductRepository productRepository)
        {
            _productDbContext = productDbContext;
            _countryRepository = countryRepository;
            _productRepository = productRepository;
        }

        //Get Country by Id
        [HttpGet("{CountryId}", Name = "GetCountry")]
        public IActionResult GetCountry(int CountryId)
        {
            try
            {
                return Ok(_countryRepository.GetCountry(CountryId));
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }

        //Get All Countries
        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                return Ok(await _countryRepository.GetCountries());
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }

        //Create new Country
        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] Country toCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _countryRepository.CreateCountry(toCreate);

                /*if (!await _productRepository.CreateProduct(toCreate))
                {
                    ModelState.AddModelError("", $"Something went wrong saving data");
                    return StatusCode(500, ModelState);
                } */
                return CreatedAtRoute("GetCountry", new Country { CountryId = toCreate.CountryId }, toCreate);
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }

        //Delete Category
        [HttpDelete("{CountryId}")]

        public async Task<IActionResult> DeleteCountry([FromRoute] int CountryId)
        {
            //var products = await _productDbContext.Products.Where(p => p.ProductId == ProductId).FirstOrDefaultAsync();
            var countries = await _productDbContext.Countries.FindAsync(CountryId);
            
            if (countries != null)
            {
                await _countryRepository.DeleteCountry(countries);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        //Update Category
        [HttpPut("{CountryId}")]
        public async Task<IActionResult> UpdateCountry(int CountryId, [FromBody]Country toUpdate)
        {
            if (toUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (CountryId != toUpdate.CountryId)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var check = await _productDbContext.Countries.FirstOrDefaultAsync(c => c.CountryId.Equals(CountryId));
            check.CountryId = toUpdate.CountryId;
            check.countryName = toUpdate.countryName;

            await _countryRepository.UpdateCountry(check);

            /*if (!await _productRepository.UpdateProduct(check))
            {
                ModelState.AddModelError("", $"Something went wrong updating data");
                return StatusCode(500, ModelState);
            } */

            return NoContent();
        }
    }
}