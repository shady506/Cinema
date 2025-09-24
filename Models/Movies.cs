using Cinema.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class Movies
    {
        public int Id { get; set; }
        
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name{ get; set; } = string.Empty;
        
        [Required]
        [MaxLength(500)]
        public string Description{ get; set; } = string.Empty ;
        
        [Required]
        [Range(0,double.MaxValue)]
        public double Price{ get; set; }

        public string ImgUrl { get; set; } = string.Empty;
        public string TrailerUrl { get; set; } = string.Empty;
        [Required]
        public DateTime StartDate{ get; set; }
        [Required]
        public DateTime EndDate{ get; set; } 
        public MovieStatus MovieStatus { get; set; }
        public int CinemaId{ get; set; }
        public int CategoryId { get; set; }
        public Cinemas? Cinema { get; set; }
        public Categories? Category { get; set; }
        public List<Actors>? Actors { get; set; }






    }
}
