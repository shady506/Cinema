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
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ProfileController(UserManager<ApplicationUser> userManager ,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var UpdateUser = user.Adapt<UpdatePersonalinfoVM>();
            return View(UpdateUser);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInfo(UpdatePersonalinfoVM model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

          
            user.Name = model.Name;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Streat = model.Streat;
            user.City = model.City;
            user.State = model.State;
            user.ZipCode = model.ZipCode;

        
            if (model.ProfileImage != null && model.ProfileImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/users");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = $"{Guid.NewGuid()}_{model.ProfileImage.FileName}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfileImage.CopyToAsync(stream);
                }

                user.ProfilePicture = "/images/users/" + fileName;
            }

          
            bool isPasswordChanging =
                !string.IsNullOrWhiteSpace(model.CurrentPassword) ||
                !string.IsNullOrWhiteSpace(model.NewPassword) ||
                !string.IsNullOrWhiteSpace(model.ConfirmPassword);

            if (isPasswordChanging)
            {
                if (!string.IsNullOrEmpty(model.CurrentPassword) &&
                    !string.IsNullOrEmpty(model.NewPassword) &&
                    !string.IsNullOrEmpty(model.ConfirmPassword))
                {
                    if (model.NewPassword == model.ConfirmPassword)
                    {
                        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                                ModelState.AddModelError(string.Empty, error.Description);

                            return View("Index", model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                        return View("Index", model);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please fill all password fields to change your password.");
                    return View("Index", model);
                }
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);


            TempData["SuccessMessage"] = "Updated Successfully";

            return RedirectToAction(nameof(Index), "Profile", new { Area = "Identity" });
        }


        [HttpPost]
        public async Task<IActionResult> RemoveProfilePicture(UpdatePersonalinfoVM model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            if (!string.IsNullOrEmpty(user.ProfilePicture))
            {
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", user.ProfilePicture.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
            }


            user.ProfilePicture = null;
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }





    }
}
