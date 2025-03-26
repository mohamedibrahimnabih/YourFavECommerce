using Microsoft.AspNetCore.Mvc;
using YourFavECommerce.Api.DTOs.Requests;
using YourFavECommerce.Api.Mapping;
using YourFavECommerce.Api.Services.IServcies;
using Mapster;
using YourFavECommerce.Api.DTOs.Responses;
using YourFavECommerce.Api.Models;
using Microsoft.AspNetCore.Authorization;

namespace YourFavECommerce.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;


        [HttpGet("")]
        public IActionResult GetAll()
        {
            var categories = _categoryService.GetAll();

            return Ok(categories.Adapt<IEnumerable<CatgeoryResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) 
        {
            var categroy = _categoryService.Get(e => e.Id == id);

            //if (categroy == null)
            //    return NotFound();

            //return Ok(categroy);

            return categroy == null ?  NotFound() : Ok(categroy.Adapt<CatgeoryResponse>());
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] CatgeoryRequest category)
        {
            //if (!ModelState.IsValid)
            //    return ValidationProblem(ModelState);

            var categoryInDb = _categoryService.Add(category.Adapt<Category>());

            //return Created($"{Request.Scheme}://{Request.Host}/api/Categories/{category.Id}", category);

            if(categoryInDb != null)
                return CreatedAtAction(nameof(GetById), new { id = categoryInDb.Id }, categoryInDb);

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CatgeoryRequest category)
        {
            //if (!ModelState.IsValid)
            //    return ValidationProblem(ModelState);

            var categroyInDb = _categoryService.Edit(id, category.Adapt<Category>());

            if (categroyInDb)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id) {
            var categroyInDb = _categoryService.Remove(id);

            if (categroyInDb)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
