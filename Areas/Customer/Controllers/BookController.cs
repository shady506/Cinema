using Cinema.Models;
using Cinema.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Threading.Tasks;

namespace Cinema.Areas.Customer.Controllers
{
    [Area(SD.CustomerArea)]
    [Authorize]
    public class BookController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<Promotion> _promotionrepository;

        public BookController(UserManager<ApplicationUser> userManager , IRepository<Ticket> ticketRepository, IRepository<Promotion> promotionrepository)
        {
            _userManager = userManager;
            _ticketRepository = ticketRepository;
            _promotionrepository = promotionrepository;
        }

        public async Task<IActionResult> BookTicket(BookRequestVM bookRequestVM)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            var ticket = await _ticketRepository.GetOne(e => e.ApplicationUserId == user.Id && e.MovieId == bookRequestVM.MovieId);
             if (ticket is not null)
            {
                ticket.Count += bookRequestVM.Count;
            }else
            {
                await _ticketRepository.CreateAsync(new()
                {
                    ApplicationUserId = user.Id,
                    MovieId = bookRequestVM.MovieId,
                    Count = bookRequestVM.Count
                });
            }

            

            await _ticketRepository.CommitAsync();


            TempData["success-Notification"] = "Ticket(s) Booked Successfully";
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        public async Task<ActionResult> Index(string? Code = null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            var Tickets = await _ticketRepository.GetAsync(e => e.ApplicationUserId == user.Id ,includes : [e => e.Movie,e=>e.Movie.Cinema]);
            var totalPrice = Tickets.Sum(e => e.Movie.Price * e.Count);
            if (Code is not null)
            {
                var Promotion = await _promotionrepository.GetOne(e => e.Code == Code);
                if (Promotion is null || !Promotion.Status || DateTime.UtcNow > Promotion.ValidTo)
                {
                    TempData["error-Notification"] = "Invalid Code Or Expired";

                }
                else 
                {
                    Promotion.TotalUsed += 1;
                    await _promotionrepository.CommitAsync();
                    totalPrice = totalPrice - (totalPrice * 0.05);
                    TempData["success-Notification"] = "Apply Promotion Successfully ";
                }

            }
          
            ViewBag.TotalPrice = totalPrice;
            return View(Tickets);
        }

        public async Task<IActionResult> IncrementTicket(int MovieId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            var ticket = await _ticketRepository.GetOne(e => e.ApplicationUserId == user.Id && e.MovieId == MovieId);
            if (ticket == null)
                return NotFound();
            ticket.Count += 1;
            await _ticketRepository.CommitAsync();
            return RedirectToAction("Index", "Book", new { area = "Customer" });

        }

        public async Task<IActionResult> DecrementTicket(int MovieId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            var ticket = await _ticketRepository.GetOne(e => e.ApplicationUserId == user.Id && e.MovieId == MovieId);
            if (ticket == null)
                return NotFound();

            if (ticket.Count > 1)
            {
                ticket.Count -= 1;
                await _ticketRepository.CommitAsync();
            }
           
            return RedirectToAction("Index", "Book", new { area = "Customer" });
        }


        public async Task<IActionResult> DeleteTicket(int MovieId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            var ticket = await _ticketRepository.GetOne(e => e.ApplicationUserId == user.Id && e.MovieId == MovieId);
            if (ticket == null)
                return NotFound();

            _ticketRepository.Delete(ticket);
            await _ticketRepository.CommitAsync();

            return RedirectToAction("Index", "Book", new { area = "Customer" });

        }

        public async Task<IActionResult> Pay()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            var Tickets = await _ticketRepository.GetAsync(e => e.ApplicationUserId == user.Id, includes: [e=>e.Movie,e=>e.Movie.Cinema]);

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/Customer/Checkout/Success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/Customer/checkout/cancel",
            };

            foreach (var item in Tickets)
            {
                options.LineItems.Add( new SessionLineItemOptions
                {
                   PriceData = new SessionLineItemPriceDataOptions
                   {
                       Currency = "egp",
                       ProductData = new SessionLineItemPriceDataProductDataOptions
                       {
                           Name = item.Movie.Name,
                           Description = item.Movie.Description,
                       },
                       UnitAmount = (long)item.Movie.Price*100,
                   },
                   Quantity = item.Count,
                });
            }

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);

        }


    }
}
