using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class Actors
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public string News { get; set; } = string.Empty;
        

        public List<Movies>? Movies { get; set; }
    }
}
