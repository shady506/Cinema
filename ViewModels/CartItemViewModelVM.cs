namespace Cinema.ViewModels
{
    public class CartItemViewModel
    {
        public int CartItemId { get; set; }
        public int ShowTimeId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public DateTime ShowDate { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
