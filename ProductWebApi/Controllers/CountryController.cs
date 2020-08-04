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
        [HttpGet("{Id}", Name = "GetCountry")]
        public async Task<IActionResult> GetCountry(int Id)
        {
            try
            {
                if (!await _countryRepository.IsCountryIdExists(Id))
                {
                    return NotFound();
                }
                var country = _countryRepository.Get(Id);
                var countryDto = new CountryDto()
                {
                    Id = country.Id,
                    Name = country.Name
                };
                return Ok(countryDto);
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }

        //Get All Countries
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            try
            {
                var countries = await _countryRepository.Gets();
                var countriesDto = new List<CountryDto>();
                foreach (var country in countries)
                {
                    countriesDto.Add(new CountryDto
                    {
                        Id = country.Id,
                        Name = country.Name
                    });
                }
                return Ok(countriesDto);
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }


        //Create new Country
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Country toCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _countryRepository.Create(toCreate);

                /*if (!await _productRepository.Create(toCreate))
                {
                    ModelState.AddModelError("", $"Something went wrong saving data");
                    return StatusCode(500, ModelState);
                } */
                return CreatedAtRoute("GetCountry", new Country { Id = toCreate.Id }, toCreate);
            }
            catch (Exception E)
            {

                return StatusCode(500, E.Message);
            }
        }

        //Delete Country
        [HttpDelete("{Id}")]

        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            var countries = await _productDbContext.Countries.FindAsync(Id);
            
            if (countries != null)
            {
                await _countryRepository.Delete(countries);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        //Update Country
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody]Country toUpdate)
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

            var check = await _productDbContext.Countries.FirstOrDefaultAsync(c => c.Id.Equals(Id));
            check.Id = toUpdate.Id;
            check.Name = toUpdate.Name;

            await _countryRepository.Update(check);

            /*if (!await _productRepository.Update(check))
            {
                ModelState.AddModelError("", $"Something went wrong updating data");
                return StatusCode(500, ModelState);
            } */

            return NoContent();
        }


    }
}