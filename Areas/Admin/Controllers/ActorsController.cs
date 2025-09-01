using Microsoft.AspNetCore.Mvc;

namespace Cinema.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]

    public class ActorsController : Controller
    {
        private ApplicationDbContext _context = new();

        public IActionResult Index()
        {
            var Actors = _context.Actors;

            return View(Actors.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Actors Actor)
        {

            _context.Actors.Add(Actor);
            _context.SaveChanges();
            TempData["success-notification"] = "Add Actor Successfully";
            return RedirectToAction(nameof(Index),"Actors");
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var Actor = _context.Actors.FirstOrDefault(e => e.Id ==Id);

            if (Actor is null)
                return RedirectToAction(SD.NotFoundPage,SD.HomeController);
            
                
            
            return View(Actor);
        }
        [HttpPost]
        public IActionResult Edit(Actors Actor)
        {
          
            _context.Actors.Update(Actor);
            _context.SaveChanges();
            TempData["success-notification"] = "Update Actor Successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete (int Id)
        {
            var Actor = _context.Actors.FirstOrDefault(e => e.Id == Id);

            if (Actor is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);


            _context.Actors.Remove(Actor);
            _context.SaveChanges();
            TempData["success-notification"] = "Delete Actor Successfully";
            return RedirectToAction(nameof(Index));
        }




    }
}
