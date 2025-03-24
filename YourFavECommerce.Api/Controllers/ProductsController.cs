using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourFavECommerce.Api.Data;
using YourFavECommerce.Api.DTOs.Requests;
using YourFavECommerce.Api.DTOs.Responses;
using YourFavECommerce.Api.Models;
using YourFavECommerce.Api.Services.IServcies;

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

        [HttpPost("")]
        public IActionResult Create([FromForm] ProductRequest productRequest)
        {
            var product = productRequest.Adapt<Product>();

            if (productRequest.File != null && productRequest.File.Length > 0)
            {
                //var filePath = Path.GetTempFileName();

                // Save in image folder
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(productRequest.File.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);

                if(!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "images")))
                {
                    System.IO.File.Create(Path.Combine(Directory.GetCurrentDirectory(), "images"));
                }

                using (var stream = System.IO.File.Create(filePath))
                {
                    productRequest.File.CopyTo(stream);
                }

                // Save in Db
                product.MainImg = fileName;
                _context.Products.Add(product);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product.Adapt<ProductResponse>());
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromForm] ProductRequest productRequest)
        {
            var productInDb = _context.Products.AsNoTracking().SingleOrDefault(e => e.Id == id);
            var product = productRequest.Adapt<Product>();

            if(productInDb != null)
            {
                if (productRequest.File != null && productRequest.File.Length > 0)
                {
                    // Save new img in image folder
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(productRequest.File.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);

                    if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "images")))
                    {
                        System.IO.File.Create(Path.Combine(Directory.GetCurrentDirectory(), "images"));
                    }

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        productRequest.File.CopyTo(stream);
                    }

                    // Delete old img from image folder
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "images", productInDb.MainImg);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }

                    // Save new img in Db
                    product.MainImg = fileName;
                }
                else
                {
                    product.MainImg = productInDb.MainImg;
                }

                product.Id = productInDb.Id;
                _context.Products.Update(product);
                _context.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var productInDb = _context.Products.SingleOrDefault(e => e.Id == id);

            if (productInDb != null)
            {
                // Delete old img from image folder
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "images", productInDb.MainImg);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                _context.Products.Remove(productInDb);
                _context.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }
    }
}
