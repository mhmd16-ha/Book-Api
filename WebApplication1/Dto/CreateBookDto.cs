using BookApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookApp.Dto
{
    public class CreateBookDto
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Poster { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public Byte CategoryId { get; set; }

    }
}
