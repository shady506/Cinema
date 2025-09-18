namespace Cinema.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ShowTimeId { get; set; }
        public ShowTime ShowTime { get; set; }

        public int SeatId { get; set; }
        public Seat Seat { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.Now;
    }

}
