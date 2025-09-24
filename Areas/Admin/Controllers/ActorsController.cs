using Cinema.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cinema.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Authorize(Roles = $"{SD.SuperAdminRole},{SD.AdminRole}")]
    public class ActorsController : Controller
    {
        //private ApplicationDbContext _context = new();
        private IRepository<Actors> _actorRepository; //= new Repository<Actors>() ;

        public ActorsController(IRepository<Actors> actorRepository)
        {
            _actorRepository = actorRepository;
        }


        public async Task<IActionResult> Index()
        {
            var Actors = await _actorRepository.GetAsync();

            return View(Actors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Actors Actor)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors);
                TempData["error-Notification"] = string.Join(" , ", errors.Select(e => e.ErrorMessage));

                return View(Actor);
            }

            await _actorRepository.CreateAsync(Actor);
            await _actorRepository.CommitAsync();

            TempData["success-notification"] = "Add Actor Successfully";
            return RedirectToAction(nameof(Index),"Actors");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var Actor = await _actorRepository.GetOne(e => e.Id ==Id);

            if (Actor is null)
                return RedirectToAction(SD.NotFoundPage,SD.HomeController);
            
                
            
            return View(Actor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Actors Actor)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors);
                TempData["error-Notification"] = string.Join(" , ", errors.Select(e => e.ErrorMessage));

                return View(Actor);
            }

            _actorRepository.Edit(Actor);
           await _actorRepository.CommitAsync();

            TempData["success-notification"] = "Update Actor Successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete (int Id)
        {
            var Actor = await _actorRepository.GetOne(e => e.Id == Id);

            if (Actor is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);

            _actorRepository.Delete(Actor);
            await _actorRepository.CommitAsync();

            TempData["success-notification"] = "Delete Actor Successfully";
            return RedirectToAction(nameof(Index));
        }




    }
}
