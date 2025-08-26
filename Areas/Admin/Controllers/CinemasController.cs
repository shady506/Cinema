using Microsoft.AspNetCore.Mvc;

namespace Cinema.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]

    public class CinemasController : Controller
    {
        private ApplicationDbContext _context = new();

        public IActionResult Index()
        {
            var Cinemas = _context.Cinemas;

            return View(Cinemas.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cinemas cinema)
        {

            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index),"Cinemas");
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(e => e.Id ==Id);

            if (cinema is null)
                return RedirectToAction(SD.NotFoundPage,SD.HomeController);
            
                
            
            return View(cinema);
        }
        [HttpPost]
        public IActionResult Edit(Cinemas cinema)
        {
          
            _context.Cinemas.Update(cinema);
            _context.SaveChanges();
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete (int Id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(e => e.Id == Id);

            if (cinema is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);


            _context.Cinemas.Remove(cinema);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }




    }
}
