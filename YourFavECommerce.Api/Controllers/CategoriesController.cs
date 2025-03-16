using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourFavECommerce.Api.Data;
using YourFavECommerce.Api.Models;

namespace YourFavECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var categories = _context.Categories.ToList();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) 
        {
            var categroy = _context.Categories.Find(id);

            //if (categroy == null)
            //    return NotFound();

            //return Ok(categroy);

            return categroy == null ?  NotFound() : Ok(categroy);
        }

        [HttpPost("")]
        public IActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            //return Created($"{Request.Scheme}://{Request.Host}/api/Categories/{category.Id}", category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, Category category)
        {
            var categroyInDb = _context.Categories.AsNoTracking().FirstOrDefault(e => e.Id == id);

            if (categroyInDb != null)
            {
                category.Id = id;

                _context.Categories.Update(category);
                _context.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id) {
            var categroy = _context.Categories.Find(id);
            if(categroy == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categroy);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
