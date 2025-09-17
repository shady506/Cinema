using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cinema.Areas.Identity.Controllers
{
    [Area(SD.IdentityArea)]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var UpdateUser = user.Adapt<UpdatePersonalinfoVM>();
            return View(UpdateUser);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInfo(UpdatePersonalinfoVM updatePersonalinfoVM)
        {
            if (!ModelState.IsValid) 
            {
                return View(updatePersonalinfoVM);
            }
            var user = await _userManager.GetUserAsync (User);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = updatePersonalinfoVM.Name;
            user.Email = updatePersonalinfoVM.Email;
            user.PhoneNumber = updatePersonalinfoVM.PhoneNumber;
            user.Streat = updatePersonalinfoVM.Streat;
            user.City = updatePersonalinfoVM.City;
            user.State = updatePersonalinfoVM.State;    
            user.ZipCode = updatePersonalinfoVM.ZipCode;  
            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index),"Profile",new { Area = "Identity"});
        
        }
    }
}
