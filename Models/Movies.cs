using Cinema.DataAccess.Enums;

namespace Cinema.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public string Name{ get; set; } = string.Empty;
        public string Description{ get; set; } = string.Empty ;
        public double Price{ get; set; }
        public string ImgUrl { get; set; } = string.Empty;
        public string TrailerUrl { get; set; } = string.Empty;
        public DateTime StartDate{ get; set; } 
        public DateTime EndDate{ get; set; } 
        public MovieStatus MovieStatus { get; set; }
        public int CinemaId{ get; set; }
        public int CategoryId { get; set; }
        public Cinemas? Cinema { get; set; }
        public Categories? Category { get; set; }
        public List<Actors>? Actors { get; set; }
    
    }
}
