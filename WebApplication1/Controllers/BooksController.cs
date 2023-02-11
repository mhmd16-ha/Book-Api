using BookApp.Data;
using BookApp.Dto;
using BookApp.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private new List<string> _allowedExtension = new List<string> { ".png", ".jpg" };
        private long _allowedSize = 1048576;
        private readonly IBookServices _book;
        private readonly ICategoryServices _cat;
        public BooksController(IBookServices book, ICategoryServices cat)
        {
            _book = book;
            _cat = cat;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var book = await _book.GetAll();
            return Ok(book);
        }
        [HttpGet("GetByCategoryIdAsync")]
        public async Task<IActionResult> GetByCategoryIdAsync(byte catId)
        {
            var book = await _book.GetAll(catId);
            return Ok(book);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            
            var book = await _book.GetById(id);
            if(book == null)
                return NotFound();
            
            GetBookDto dto = new GetBookDto
            {
                Title=book.Title,
                Year=book.Year,
                Rate=book.Rate,
                Poster=book.Poster,
                Description=book.Description,
                Category=book.category.Name,
            };
            return Ok(dto);
        }
        [HttpPost]
        public async Task<IActionResult> CreatBook([FromForm] CreateBookDto dto)
        {
            if(dto.Poster==null)
                return BadRequest("Poster is required!");
            if (dto.Poster == null)
                return BadRequest("Poster is required!");
            if (_allowedSize < dto.Poster.Length)
            {
                return BadRequest("Only 1 Mega");
            }
            if (!_allowedExtension.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
            {
                return BadRequest("Only jpg png");
            }
            var isVaildCategpry =await _cat.isVaildgenra(dto.CategoryId);
            if (!isVaildCategpry)
                return BadRequest("Not Vaild Id");
            using var dataStreem = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStreem);
            Book b=new Book
            {
                Title=dto.Title,
                Description=dto.Description,
                Poster= dataStreem.ToArray(),
                CategoryId=dto.CategoryId,
                Year=dto.Year,
                Rate=dto.Rate,
            } ;
           await _book.Add(b);
            return Ok(b);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var b= await _book.GetById(id);
            if (b == null)
                return NotFound($"No Book With is Id {id}");
            _book.Delete(b);
            return Ok(b);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromForm] CreateBookDto dto)
        {
            var b = await _book.GetById(id);

            if (dto.Poster == null)
                return BadRequest("Poster is required!");
            if (dto.Poster == null)
                return BadRequest("Poster is required!");
            if (_allowedSize < dto.Poster.Length)
            {
                return BadRequest("Only 1 Mega");
            }
            if (!_allowedExtension.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
            {
                return BadRequest("Only jpg png");
            }
            if (b == null)
                return NotFound($"No Book With is Id {id}");

            var isVaildCategpry = await _cat.isVaildgenra(dto.CategoryId);
            if (!isVaildCategpry)
                return BadRequest("Not Vaild Id");
            using var dataStreem = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStreem);
            b.Poster=dataStreem.ToArray();
            b.Title = dto.Title;
            b.Description = dto.Description;
            b.Rate=dto.Rate;
            b.CategoryId=dto.CategoryId;
            b.Year=dto.Year;
            _book.Updata(b);
            return Ok(b);
        }
    }
}
