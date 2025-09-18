namespace Cinema.Models
{
    public class ShowTime
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }

        public int MovieId { get; set; }
        public Movies Movie { get; set; }
        public decimal Price{ get; set; }
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    }


}
