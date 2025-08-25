namespace Cinema.Models
{
    public class Categories
    {
        public int Id{ get; set; }
        public string Name{ get; set; } = string.Empty;

        public List<Movies>? Movies { get; set; }
    }
}
