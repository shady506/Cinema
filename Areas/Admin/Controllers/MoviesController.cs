using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context = new();


        public IActionResult Index()
        {
            var Movies = _context.Movies.Include(e=>e.Category).Include(e=>e.Cinema);
            return View(Movies.ToList());
        }





        [HttpGet]
        public IActionResult Create()
        {
            var categories = _context.Categories;
            var Cinemas= _context.Cinemas;

            CategoriesWithCinemasVM CategoriesWithCinemasVM = new()
            {
                Categories = categories.ToList(),
                Cinemas= Cinemas.ToList(),
            };
            return View(CategoriesWithCinemasVM);
        }
        [HttpPost]
        public IActionResult Create(Movies Movie,IFormFile ImgUrl)
        {
            if (ImgUrl is not null && ImgUrl.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\movies", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ImgUrl.CopyTo(stream);
                }
                Movie.ImgUrl = fileName;
                _context.Movies.Add(Movie);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }



        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.FirstOrDefault(e => e.Id == id);

            var categories = _context.Categories;
            var Cinemas = _context.Cinemas;

            CategoriesWithCinemasVM CategoriesWithCinemasVM = new()
            {
                Movie = movie,
                Categories = categories.ToList(),
                Cinemas = Cinemas.ToList(),
            };
            if (movie is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);

            return View(CategoriesWithCinemasVM);
        }
        [HttpPost]
        public IActionResult Edit(Movies movie, IFormFile? ImgUrl)
        {
            var MovieDb = _context.Movies.AsNoTracking().FirstOrDefault(e => e.Id == movie.Id);
            if (MovieDb is null)
                return BadRequest();
            if (ImgUrl is not null && ImgUrl.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ImgUrl.CopyTo(stream);
                }

                var OldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", MovieDb.ImgUrl);

                if (System.IO.File.Exists(OldFilePath))
                {
                    System.IO.File.Delete(OldFilePath);
                }

                movie.ImgUrl = fileName;
            }
            else
            {
                movie.ImgUrl = MovieDb.ImgUrl;
            }

            _context.Movies.Update(movie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



        public ActionResult Delete(int id)
        {
            var movie = _context.Movies.FirstOrDefault(e => e.Id == id);

            if (movie is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);

            var OldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", movie.ImgUrl);

            if (System.IO.File.Exists(OldFilePath))
            {
                System.IO.File.Delete(OldFilePath);
            }
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
