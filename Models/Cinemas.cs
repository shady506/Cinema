namespace Cinema.Models
{
    public class Cinemas
    {
        public int Id{ get; set; }
        public string Name{ get; set; } = string.Empty;
        public string Description{ get; set; } = string.Empty ;
        public string CinemaLogo{ get; set; }=string.Empty ;    
        public string Address { get; set; } = string.Empty;
        
        public List<Movies>? Movies{ get; set; } 

    }
}
