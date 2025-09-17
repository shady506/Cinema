using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Areas.Customer.Controllers
{

    [Area(SD.CustomerArea)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ApplicationDbContext _context;//= new();
        public HomeController(ILogger<HomeController> logger , ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }







        public IActionResult NotFoundPage()
        {
            return View();
        }



        public IActionResult Index(FilteredMoviesVM filteredMovies,int page = 1)
        {
            var Movies = _context.Movies.Include(e=>e.Cinema).Include(e=>e.Category).AsQueryable();
            if (filteredMovies.MovieName is not null)
            {
                Movies = Movies.Where(e => e.Name.Contains(filteredMovies.MovieName));
                ViewBag.Name = filteredMovies.MovieName;
            }


            double totalPages = Math.Ceiling(Movies.Count() / 8.0); 
            int currentPage = page;

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = currentPage;

            Movies= Movies.Skip((page - 1) * 8).Take(8);
            return View(Movies.ToList());
        }
        public IActionResult Details(int Id)
        {
            var movie = _context.Movies.Include(e => e.Cinema).Include(e => e.Category).Include(e=>e.Actors).FirstOrDefault(e => e.Id == Id);
            if (movie == null)
                return NotFound();

            return View(movie);
        }
        public IActionResult ActorDetails(FilteredMoviesVM filteredMovies)
        {
            var actor = _context.Actors.Include(e => e.Movies).FirstOrDefault(e => e.Id == filteredMovies.Id);

            if (actor is null)
                return NotFound();

            var relatedMovies = actor.Movies.ToList();

            ViewBag.RelatedMovies = relatedMovies;

            return View(actor);
        }
        public IActionResult Categories(int Id)
        {
            var Categories = _context.Categories.Include(e=>e.Movies).AsQueryable();

            var relatedCategories = Categories.Include(e=>e.Movies).ToList();
            ViewBag.RelatedCategories = relatedCategories;

            return View(Categories.ToList());
        }
        public IActionResult Cinemas(int Id)
        {
            var Cinemas= _context.Cinemas.AsQueryable();
            return View(Cinemas.ToList());
        }




















        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
