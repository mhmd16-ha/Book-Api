using System.ComponentModel.DataAnnotations.Schema;

namespace BookApp.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Byte Id { get; set; }
        public string Name { get; set; }
    }
}
