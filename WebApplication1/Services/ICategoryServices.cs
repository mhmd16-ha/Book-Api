using BookApp.Models;

namespace BookApp.Services
{
    public interface ICategoryServices
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(byte id);
        Task<Category> Add(Category cat);
        Category Update(Category cat);
        Category Delete(Category cat);
        Task<bool> isVaildgenra(byte id);
    }
}
