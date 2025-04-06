using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourFavECommerce.Api.DTOs.Requests;
using YourFavECommerce.Api.DTOs.Responses;
using YourFavECommerce.Api.Models;
using YourFavECommerce.Api.Services.IServcies;
using Mapster;
namespace YourFavECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController(IBrandServices brandService) : ControllerBase
    {
        private readonly IBrandServices _brandService = brandService;


        [HttpGet("")]
        public IActionResult GetAll()
        {
            var brands = _brandService.GetAll();

            return Ok(brands.Adapt<IEnumerable<BrandResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var barnd = _brandService.Get(e => e.Id == id);

      

            return barnd == null ? NotFound() : Ok(barnd.Adapt<BrandResponse>());
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] BrandRequest brand)
        {

            var brandInDb = _brandService.Add(brand.Adapt<Brand>());


            if (brandInDb != null)
                return CreatedAtAction(nameof(GetById), new { id = brandInDb.Id }, brandInDb);

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] BrandRequest brand)
        {

            var brandInDb = _brandService.Edit(id, brand.Adapt<Brand>());

            if (brandInDb)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var brandInDb = _brandService.Remove(id);

            if (brandInDb)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
