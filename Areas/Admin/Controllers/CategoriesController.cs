using Microsoft.AspNetCore.Mvc;

namespace Cinema.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]

    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context = new();

        public IActionResult Index()
        {
            var Categories = _context.Categories;

            return View(Categories.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Categories category)
        {

            _context.Categories.Add(category);
            _context.SaveChanges();
            TempData["success-notification"] = "Add Category Successfully";
            return RedirectToAction(nameof(Index),"Categories");
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var category = _context.Categories.FirstOrDefault(e => e.Id ==Id);

            if (category is null)
                return RedirectToAction(SD.NotFoundPage,SD.HomeController);
            
                
            
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Categories category)
        {
          
            _context.Categories.Update(category);
            _context.SaveChanges();
            TempData["success-notification"] = "Update Category Successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete (int Id)
        {
            var category = _context.Categories.FirstOrDefault(e => e.Id == Id);

            if (category is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);


            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["success-notification"] = "Delete Category Successfully";
            return RedirectToAction(nameof(Index));
        }




    }
}
