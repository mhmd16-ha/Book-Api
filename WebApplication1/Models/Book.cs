using System.ComponentModel.DataAnnotations.Schema;

namespace BookApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[]? Poster { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public Byte CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category category { get; set; }

    }
}
