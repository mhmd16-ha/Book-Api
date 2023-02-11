using BookApp.Data;
using BookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ApplicationDbContext _context;
        public CategoryServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Category> Add(Category cat)
        {
           await _context.Categories.AddAsync(cat);
            _context.SaveChanges();
            return cat;
        }

        public Category Delete(Category cat)
        {
            _context.Categories.Remove(cat);
            _context.SaveChanges();
            return cat;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var cat = await _context.Categories.ToListAsync();
            return (cat);
        }

        public async Task<Category> GetByIdAsync(byte id)
        {
            var cat=await _context.Categories.SingleOrDefaultAsync(x=>x.Id==id);
            return (cat);

        }

        public Task<bool> isVaildgenra(byte id)
        {
            return _context.Categories.AnyAsync(g => g.Id == id);
        }

        public Category Update(Category cat)
        {
            _context.Categories.Update(cat);
            _context.SaveChanges();
            return cat;
        }
    }
}
