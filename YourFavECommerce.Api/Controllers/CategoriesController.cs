﻿using Microsoft.AspNetCore.Mvc;
using YourFavECommerce.Api.DTOs.Requests;
using YourFavECommerce.Api.Mapping;
using YourFavECommerce.Api.Services.IServcies;
using Mapster;
using YourFavECommerce.Api.DTOs.Responses;
using YourFavECommerce.Api.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using YourFavECommerce.Api.Validator;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FluentValidation;

namespace YourFavECommerce.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;


        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAsync();

            return Ok(categories.Adapt<IEnumerable<CatgeoryResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) 
        {
            var categroy = _categoryService.GetOne(e => e.Id == id);

            //if (categroy == null)
            //    return NotFound();

            //return Ok(categroy);

            return categroy == null ?  NotFound() : Ok(categroy.Adapt<CatgeoryResponse>());
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CatgeoryRequest category, CancellationToken cancellationToken, [FromServices] IValidator<CatgeoryRequest> validator)
        {
            //if (!ModelState.IsValid)
            //    return ValidationProblem(ModelState);

            //ValidationResult results = await validator.ValidateAsync(category);

            //if(!results.IsValid)
            //{
            //    ModelStateDictionary keyValuePairs = new();
            //    foreach (var failure in results.Errors)
            //    {
            //        keyValuePairs.AddModelError(failure.PropertyName, failure.ErrorMessage);
            //    }

            //    return ValidationProblem(keyValuePairs);
            //}

            var categoryInDb = await _categoryService.AddAsync(category.Adapt<Category>(), cancellationToken);
            await _categoryService.CommitAsync();

            //return Created($"{Request.Scheme}://{Request.Host}/api/Categories/{category.Id}", category);

            if(categoryInDb != null)
                return CreatedAtAction(nameof(GetById), new { id = categoryInDb.Id }, categoryInDb);

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CatgeoryRequest category)
        {
            //if (!ModelState.IsValid)
            //    return ValidationProblem(ModelState);

            var categroyInDb = await _categoryService.EditAsync(id, category.Adapt<Category>());

            if (categroyInDb)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            var categroyInDb = _categoryService.Remove(id);

            if (categroyInDb)
            {
                await _categoryService.CommitAsync();
                return NoContent();
            }

            return NotFound();
        }
    }
}
