using Cinema.Models;
using Cinema.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

 
        public TicketController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

       
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var cartItems = await _context.cartItems
                .Include(c => c.Seat)
                .Include(c => c.ShowTime)
                .ThenInclude(st => st.Movie)
                .Where(c => c.UserId == userId)
                .Select(c => new CartItemViewModel
                {
                    CartItemId = c.Id,
                    ShowTimeId = c.ShowTimeId,
                    MovieTitle = c.ShowTime.Movie.Name,
                    ShowDate = c.ShowTime.StartTime,
                    SeatNumber = c.Seat.SeatNumber,
                    Price = c.ShowTime.Price
                })
                .ToListAsync();

            return View(cartItems);
        }

    
        [HttpPost]
        public async Task<IActionResult> AddToCart(int showTimeId, int seatId)
        {
            var userId = _userManager.GetUserId(User);

            var seat = await _context.seats
                .FirstOrDefaultAsync(s => s.Id == seatId && s.ShowTimeId == showTimeId);

            if (seat == null || seat.IsBooked)
                return BadRequest("This seat is already booked or not available.");

            var existingCartItem = await _context.cartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.SeatId == seatId && c.ShowTimeId == showTimeId);

            if (existingCartItem != null)
                return BadRequest("This seat is already in your cart.");

            var cartItem = new CartItem
            {
                UserId = userId,
                ShowTimeId = showTimeId,
                SeatId = seatId,
                AddedDate = DateTime.Now
            };

            _context.cartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var userId = _userManager.GetUserId(User);

            var cartItem = await _context.cartItems
                .FirstOrDefaultAsync(c => c.Id == cartItemId && c.UserId == userId);

            if (cartItem == null)
                return NotFound();

            _context.cartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var userId = _userManager.GetUserId(User);

            var cartItems = await _context.cartItems
                .Include(c => c.Seat)
                .Include(c => c.ShowTime)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
                return View("CheckoutSuccess", "Your cart is empty!");

            foreach (var item in cartItems)
            {
                var booking = new Booking
                {
                    UserId = userId,
                    ShowTimeId = item.ShowTimeId,
                    SeatId = item.SeatId,
                    BookingDate = DateTime.Now
                };

                _context.bookings.Add(booking);
                item.Seat.IsBooked = true;
            }

            _context.cartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return View("CheckoutSuccess");
        }
    }
}
