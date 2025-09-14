using Cinema.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Threading.Tasks;

namespace Cinema.Areas.Identity.Controllers
{
    [Area(SD.IdentityArea)]
    public class AccountController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager , IEmailSender emailSender , SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
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

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            var Link = Url.Action("ConfirmEmail", "Account", new
            {
                area = "Identity",
                token = token,
                userId = applicationUser.Id
            } ,Request.Scheme);

           await _emailSender.SendEmailAsync(applicationUser.Email,"Confirm Your Email!",
                $"<h1>Confirm Your Email By Click <a href ='{Link}'>Here</a></h1>");

            TempData["success-Notification"] = "Create User Successfully , Please Confirm Your Email";

            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }



        public async Task<IActionResult> ConfirmEmail(string token , string userId)
        {
            var user =  await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();
           var result = await _userManager.ConfirmEmailAsync(user,token);

            if (!result.Succeeded)
            {
                TempData["error-Notification"] = "Link Expired!,Resend Email Confirmation";

            }
            else
            {
                TempData["success-Notification"] = "Confirm Email Successfully";
            }

            

            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {

            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByEmailAsync(loginVM.EmailOrUserName) ?? await _userManager.FindByNameAsync(loginVM.EmailOrUserName);

            if (user == null)
            {
                TempData["error-Notification"] = "Invalid User Name Or Password";
                return View(loginVM);
            }

            var result = await _signInManager.PasswordSignInAsync(user,loginVM.Password,loginVM.RememberMe,true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    TempData["error-Notification"] = "To Many Attempts";
                }

                TempData["error-Notification"] = "Invalid User Name Or Password";
                return View(loginVM);

            }
            if (!user.EmailConfirmed)
            {
                TempData["error-Notification"] = "Confirm Your Email First !";
                return View(loginVM);

            }
            TempData["success-Notification"] = "Login Successfully ";

            return RedirectToAction("Index", "Home", new { area = "Customer" });

        }
        

    }
}
