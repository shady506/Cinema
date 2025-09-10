using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cinema.Areas.Identity.Controllers
{
    [Area(SD.IdentityArea)]
    public class AccountController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            ApplicationUser applicationUser = new()
            {
                Name = registerVM.Name,
                Email = registerVM.Email,
                Address = registerVM.Address,
                UserName = registerVM.UserName
            };
            //ApplicationUser applicationUser = registerVM.Adapt<ApplicationUser>();

            var Result = await _userManager.CreateAsync(applicationUser,registerVM.Password);
            if (!Result.Succeeded) 
            {
                foreach (var item in Result.Errors)
                {
                    ModelState.AddModelError(String.Empty, item.Description);
                }
                return View(registerVM);
            
            }

            TempData["success-Notification"] = "Create User Successfully";

            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }
    }
}
