using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class Cinemas
    {
        public int Id{ get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name{ get; set; } = string.Empty;
        public string Description{ get; set; } = string.Empty ;
        public string CinemaLogo{ get; set; }=string.Empty ;    
        public string Address { get; set; } = string.Empty;
        
        public List<Movies>? Movies{ get; set; } 

    }
}
