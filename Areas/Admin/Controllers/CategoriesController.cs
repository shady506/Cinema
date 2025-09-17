using Cinema.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cinema.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Authorize(Roles = $"{SD.SuperAdminRole},{SD.AdminRole}")]
    public class CategoriesController : Controller
    {
        //private ApplicationDbContext _context = new();
        private IRepository<Categories> _categoryRepository; //= new Repository<Categories>(); 

        public CategoriesController(IRepository<Categories> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            var Categories = await _categoryRepository.GetAsync();

            return View(Categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Categories category)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors);
                TempData["error-Notification"] = string.Join(" , ", errors.Select(e => e.ErrorMessage));

                return View(category);
            }

            await _categoryRepository.CreateAsync(category);
            await _categoryRepository.CommitAsync();

            TempData["success-notification"] = "Add Category Successfully";
            return RedirectToAction(nameof(Index),"Categories");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
           
                var category = await _categoryRepository.GetOne(e => e.Id ==Id);

            if (category is null)
                return RedirectToAction(SD.NotFoundPage,SD.HomeController);
            
                
            
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Categories category)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors);
                TempData["error-Notification"] = string.Join(" , ", errors.Select(e => e.ErrorMessage));

                return View(category);
            }
            _categoryRepository.Edit(category);
            await _categoryRepository.CommitAsync();

            TempData["success-notification"] = "Update Category Successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete (int Id)
        {
            var category = await _categoryRepository.GetOne(e => e.Id == Id);

            if (category is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);


            _categoryRepository.Delete(category);    
            await _categoryRepository.CommitAsync();

            TempData["success-notification"] = "Delete Category Successfully";
            return RedirectToAction(nameof(Index));
        }




    }
}
