using BookApp.Models;

namespace BookApp.Services
{
    public interface IBookServices
    {
        Task<IEnumerable<Book>> GetAll(byte CategoryId=0);
        Task<Book> GetById(int id);
        Task<Book> Add(Book book);
        Book Updata(Book book);
        Book Delete(Book book);
       


    }
}
