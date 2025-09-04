using Cinema.Models;
using Cinema.ViewModels;
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
                Movie=new Movies(),
                Categories = categories.ToList(),
                Cinemas = Cinemas.ToList(),
            }; 
            return View(CategoriesWithCinemasVM);
        }
        [HttpPost]
        public IActionResult Create(CategoriesWithCinemasVM CategoriesWithCinemasVM, IFormFile ImgUrl)
        {
           
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors);
                TempData["error-Notification"] = string.Join(" , ", errors.Select(e => e.ErrorMessage));

                CategoriesWithCinemasVM.Categories = _context.Categories.ToList();
                CategoriesWithCinemasVM.Cinemas = _context.Cinemas.ToList();



                return View(CategoriesWithCinemasVM);
            }
            if (ImgUrl is not null && ImgUrl.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\movies", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ImgUrl.CopyTo(stream);
                }
                CategoriesWithCinemasVM.Movie.ImgUrl = fileName;
                _context.Movies.Add(CategoriesWithCinemasVM.Movie);
                _context.SaveChanges();

                TempData["success-notification"] = "Add Movie Successfully";
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }



        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.FirstOrDefault(e => e.Id == id);

            var categories = _context.Categories;
            var Cinemas = _context.Cinemas;

            if (movie is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);

            CategoriesWithCinemasVM CategoriesWithCinemasVM = new()
            {
                Movie = movie,
                Categories = categories.ToList(),
                Cinemas = Cinemas.ToList(),
            };
        

            return View(CategoriesWithCinemasVM);
        }

        [HttpPost]
        
        public IActionResult Edit(CategoriesWithCinemasVM vm, IFormFile? ImgUrl)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors);
                TempData["error-Notification"] = string.Join(" , ", errors.Select(e => e.ErrorMessage));

                vm.Categories = _context.Categories.ToList();
                vm.Cinemas = _context.Cinemas.ToList();

                return View(vm);
            }

            var movieDb = _context.Movies.FirstOrDefault(e => e.Id == vm.Movie.Id);
            if (movieDb is null)
                return NotFound();

         
            movieDb.Name = vm.Movie.Name;
            movieDb.Description = vm.Movie.Description;
            movieDb.Price = vm.Movie.Price;
            movieDb.StartDate = vm.Movie.StartDate;
            movieDb.EndDate = vm.Movie.EndDate;
            movieDb.CategoryId = vm.Movie.CategoryId;
            movieDb.CinemaId = vm.Movie.CinemaId;

          
            if (ImgUrl != null && ImgUrl.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ImgUrl.CopyTo(stream);
                }

                if (!string.IsNullOrEmpty(movieDb.ImgUrl))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", movieDb.ImgUrl);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                movieDb.ImgUrl = fileName;
            }

            _context.SaveChanges();
            TempData["success-notification"] = "Movie updated successfully.";
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

             TempData["success-notification"] = "Delete Movie Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
