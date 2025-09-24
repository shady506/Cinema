using Cinema.Models;
using Cinema.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cinema.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Authorize(Roles = $"{SD.SuperAdminRole},{SD.AdminRole}")]
    public class MoviesController : Controller
    {
        //private ApplicationDbContext _context = new();

        // private Repository<Movies> _movierepository =new();
        private IMovieRepository _movieRepository;// = new MovieRepository();
        private IRepository<Actors> _actorRepository;// = new Repository<Actors>();
        private IRepository<Categories> _categoryRepository;// = new Repository<Categories>();
        private IRepository<Cinemas> _cinemaRepository; //= new Repository<Cinemas>();

        public MoviesController(IMovieRepository movieRepository ,
            IRepository<Actors> actorRepository
            , IRepository<Categories> categoryRepository
            , IRepository<Cinemas> cinemaRepository)

        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
            _categoryRepository = categoryRepository;
            _cinemaRepository = cinemaRepository;
        }



        public async Task<IActionResult> Index()
        {
            var Movies = await _movieRepository.GetAsync(includes: [e=>e.Category , e=>e.Cinema]);
            return View(Movies);
        }




        [HttpGet]
        public async Task<IActionResult> Create()
        {
            
            var categories = await _categoryRepository.GetAsync();
            var Cinemas = await _cinemaRepository.GetAsync();

            CategoriesWithCinemasVM CategoriesWithCinemasVM = new()
            {
                Movie=new Movies(),
                Categories = categories,
                Cinemas = Cinemas,
            }; 
            return View(CategoriesWithCinemasVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoriesWithCinemasVM CategoriesWithCinemasVM, IFormFile ImgUrl)
        {
           
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors);
                TempData["error-Notification"] = string.Join(" , ", errors.Select(e => e.ErrorMessage));

                CategoriesWithCinemasVM.Categories = await _categoryRepository.GetAsync();
                CategoriesWithCinemasVM.Cinemas = await _cinemaRepository.GetAsync();



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

                await _movieRepository.CreateAsync(CategoriesWithCinemasVM.Movie);
                await _movieRepository.CommitAsync();

                TempData["success-notification"] = "Add Movie Successfully";
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }



        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movieRepository.GetOne(e => e.Id == id);

            var categories = await _categoryRepository.GetAsync();
            var Cinemas = await _cinemaRepository.GetAsync();

            if (movie is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);

            CategoriesWithCinemasVM CategoriesWithCinemasVM = new()
            {
                Movie = movie,
                Categories = categories,
                Cinemas = Cinemas,
            };
        

            return View(CategoriesWithCinemasVM);
        }

        [HttpPost]
        
        public async Task<IActionResult> Edit(CategoriesWithCinemasVM vm, IFormFile? ImgUrl)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors);
                TempData["error-Notification"] = string.Join(" , ", errors.Select(e => e.ErrorMessage));

                vm.Categories = await _categoryRepository.GetAsync();
                vm.Cinemas = await _cinemaRepository.GetAsync();

                return View(vm);
            }

            var movieDb = await _movieRepository.GetOne(e => e.Id == vm.Movie.Id);
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
            
            await _movieRepository.CommitAsync();

            TempData["success-notification"] = "Movie updated successfully.";
            return RedirectToAction(nameof(Index));
        }



        public async Task<ActionResult> Delete(int id)
        {
            var movie = await _movieRepository.GetOne(e => e.Id == id);

            if (movie is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);

            var OldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", movie.ImgUrl);

            if (System.IO.File.Exists(OldFilePath))
            {
                System.IO.File.Delete(OldFilePath);
            }
            _movieRepository.Delete(movie);
            await _movieRepository.CommitAsync();

             TempData["success-notification"] = "Delete Movie Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
