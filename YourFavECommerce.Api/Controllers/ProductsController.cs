using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourFavECommerce.Api.Data;
using YourFavECommerce.Api.DTOs.Requests;
using YourFavECommerce.Api.DTOs.Responses;
using YourFavECommerce.Api.Models;

namespace YourFavECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var products = _context.Products.ToList();

            return Ok(products.Adapt<IEnumerable<ProductResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var product = _context.Products.Find(id);

            if(product != null)
                return Ok(product.Adapt<ProductResponse>());

            return NotFound();
        }

        [HttpPost]
        public IActionResult Create([FromForm] ProductRequest productRequest)
        {
            var product = productRequest.Adapt<Product>();

            if (productRequest.file != null && productRequest.file.Length > 0)
            {
                //var filePath = Path.GetTempFileName();

                // Save in image folder
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(productRequest.file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    productRequest.file.CopyTo(stream);
                }

                // Save in Db
                product.MainImg = fileName;
                _context.Products.Add(product);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }

            return BadRequest();
        }
    }
}
