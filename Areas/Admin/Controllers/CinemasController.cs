using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cinema.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Authorize(Roles = $"{SD.SuperAdminRole},{SD.AdminRole}")]
    public class CinemasController : Controller
    {
        //private ApplicationDbContext _context = new();
        private IRepository<Cinemas> _cinemaRepository; // = new Repository<Cinemas> ();

        public CinemasController(IRepository<Cinemas> cinemaRepository)
        {
            _cinemaRepository = cinemaRepository;
        }
        public async Task<IActionResult> Index()
        {
            var Cinemas = await _cinemaRepository.GetAsync();

            return View(Cinemas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cinemas cinema)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors);
                TempData["error-Notification"] = string.Join(" , ", errors.Select(e => e.ErrorMessage));

                return View(cinema);
            }


            await _cinemaRepository.CreateAsync(cinema);
            await _cinemaRepository.CommitAsync();

            TempData["success-notification"] = "Add Cinema Successfully";
            return RedirectToAction(nameof(Index),"Cinemas");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var cinema = await _cinemaRepository.GetOne(e => e.Id ==Id);

            if (cinema is null)
                return RedirectToAction(SD.NotFoundPage,SD.HomeController);
            
                
            
            return View(cinema);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Cinemas cinema)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors);
                TempData["error-Notification"] = string.Join(" , ", errors.Select(e => e.ErrorMessage));

                return View(cinema);
            }
           _cinemaRepository.Edit(cinema);
            await _cinemaRepository.CommitAsync();

            TempData["success-notification"] = "Update Cinema Successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete (int Id)
        {
            var cinema = await _cinemaRepository.GetOne(e => e.Id == Id);

            if (cinema is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);


           _cinemaRepository.Delete(cinema);
            await _cinemaRepository.CommitAsync();

            TempData["success-notification"] = "Delete Cinema Successfully";
            return RedirectToAction(nameof(Index));
        }




    }
}
