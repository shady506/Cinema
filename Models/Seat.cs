namespace Cinema.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public bool IsBooked { get; set; }

        public int ShowTimeId { get; set; }
        public ShowTime ShowTime { get; set; }

        public int MovieId { get; set; }
        public Movies Movie { get; set; }
    }

}
