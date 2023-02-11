namespace BookApp.Dto
{
    public class GetBookDto
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public Byte[]? Poster { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public string Category { get; set; }
    }
}
