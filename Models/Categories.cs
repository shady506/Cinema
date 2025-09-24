using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class Categories
    {
        public int Id{ get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name{ get; set; } = string.Empty;

        public List<Movies>? Movies { get; set; }
    }
}
