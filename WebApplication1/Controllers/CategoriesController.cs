using BookApp.Data;
using BookApp.Dto;
using BookApp.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _cat;
        public CategoriesController(ICategoryServices cat )
        {
            _cat = cat;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var cat =await _cat.GetAllAsync();
            return Ok(cat);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCategoryDto dto)
        {
            var cat = new Category { Name = dto.Name };
            await _cat.Add(cat);
            return Ok(cat);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(byte id,[FromBody]CreateCategoryDto dto)
        {
            var cat = await _cat.GetByIdAsync(id);
            if (cat == null)
                return NotFound($"No Category With is Id {id}");
            cat.Name = dto.Name;
            _cat.Update(cat);
            return Ok(cat);


        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var cat =await _cat.GetByIdAsync(id);
            if (cat == null)
                return NotFound($"No Category With is Id {id}");
            _cat.Delete(cat);
            return Ok(cat);
        }
    }
}
