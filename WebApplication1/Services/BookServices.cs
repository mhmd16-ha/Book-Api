using BookApp.Data;
using BookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services
{
    public class BookServices : IBookServices
    {
        private readonly ApplicationDbContext _context;
        public BookServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Book> Add(Book book)
        {
            await _context.AddAsync(book);
            _context.SaveChanges();
            return book;

        }

        public Book Delete(Book book)
        {
            _context.Remove(book);
            _context.SaveChanges();
            return book;
        }

        public async Task<IEnumerable<Book>> GetAll(byte CatId = 0)
        {
            var book = await _context.Books.Where(m => m.CategoryId == CatId || CatId == 0).Include(x=>x.category).ToListAsync();
            return book;
        }

        public async Task<Book> GetById(int id)
        {
            var book = await _context.Books.Include(x=>x.category).SingleOrDefaultAsync(x=>x.Id==id);
            return book;
        }


        public Book Updata(Book book)
        {
            _context.Update(book);
            _context.SaveChanges();
            return book;
        }
    }
}
