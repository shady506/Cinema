using Cinema.Models;
using Cinema.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cinema.Areas.Identity.Controllers
{
    [Area(SD.IdentityArea)]
    public class AccountController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRepository<UserOTP> _userOTP;

        public AccountController(UserManager<ApplicationUser> userManager , IEmailSender emailSender , SignInManager<ApplicationUser> signInManager,IRepository<UserOTP> userOTP )
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _userOTP = userOTP;
        }

        [HttpGet]
        [UserAuthenticatedFilter]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
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
                City = registerVM.City,
                Streat= registerVM.Streat ,
                State = registerVM.State,
                ZipCode= registerVM.ZipCode,
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

            await _userManager.AddToRoleAsync(applicationUser, SD.CustomerRole);

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

            return RedirectToAction("Login", "Account", new { area = "Identity" });
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
        [UserAuthenticatedFilter]
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

            if (!user.LockoutEnabled)
            {
                TempData["error-Notification"] = $"You Have Locked Till {user.LockoutEnd}";
                return View(loginVM);

            }
            TempData["success-Notification"] = "Login Successfully ";

            return RedirectToAction("Index", "Home", new { area = "Customer" });

        }


        [HttpGet]
        public IActionResult ResendEmailConfirmation()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> ResendEmailConfirmation(ResendEmailConfirmationVM resendEmailConfirmationVM)
        {

            if (!ModelState.IsValid)
            {
                return View(resendEmailConfirmationVM);
            }

            var user = await _userManager.FindByEmailAsync(resendEmailConfirmationVM.EmailOrUserName) ?? await _userManager.FindByNameAsync(resendEmailConfirmationVM.EmailOrUserName);

            if (user == null)
            {
                TempData["error-Notification"] = "Invalid User Name Or Password";
                return View(resendEmailConfirmationVM);
            }

            if (user.EmailConfirmed)
            {
                TempData["error-Notification"] = "Allready Confirmed";
                return View(resendEmailConfirmationVM);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var Link = Url.Action("ConfirmEmail", "Account", new
            {
                area = "Identity",
                token = token,
                userId = user.Id
            }, Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email!, "Confirm Your Email!",
                 $"<h1>Confirm Your Email By Click <a href ='{Link}'>Here</a></h1>");

            TempData["success-Notification"] = "Send Email Successfully , Please Confirm Your Email";

            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }




        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]


        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM forgetPasswordVM)
        {

            if (!ModelState.IsValid)
            {
                return View(forgetPasswordVM);
            }

            var user = await _userManager.FindByEmailAsync(forgetPasswordVM.EmailOrUserName) ?? await _userManager.FindByNameAsync(forgetPasswordVM.EmailOrUserName);

            if (user == null)
            {
                TempData["error-Notification"] = "Invalid User Name Or Password";
                return View(forgetPasswordVM);
            }


            var OTPNumber = new Random().Next(1000, 9999);
            var Link = Url.Action("ResetPassword", "Account", new
            {
                area = "Identity",
                userId = user.Id
            }, Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email!, "Reset Password!",
                 $"<h1>Reset Password Using This Code {OTPNumber}. Please Don't Share It</h1>");
            await _userOTP.CreateAsync(new()
            {
                ApplicationUserId = user.Id,
                OTPNumber = OTPNumber.ToString(),
                ValidTo = DateTime.UtcNow.AddDays(1)
            });

            await _userOTP.CommitAsync();

            TempData["success-Notification"] = "Send OTP Number To Your Email Successfully ";
            return RedirectToAction("ResetPassword", "Account", new { area = "Identity" , UserId = user.Id});
        }

        
        [HttpGet]
        public IActionResult ResetPassword(string UserId )
        {
            return View(new ResetPasswordVM
            {
                ApplicationUserId = UserId
            });
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordVM);
            }
            var user = await _userManager.FindByIdAsync(resetPasswordVM.ApplicationUserId);

            if (user == null)
            {
                TempData["error-Notification"] = "Invalid User Name Or Email";
                return View(resetPasswordVM);
            }
           var userOTP = (await _userOTP.GetAsync(e => e.ApplicationUserId == resetPasswordVM.ApplicationUserId)).OrderBy(e=>e.Id).LastOrDefault();

            if (userOTP is null)
                return NotFound();  
            
            if (userOTP.OTPNumber != resetPasswordVM.OTPNumber) 
            {
                TempData["error-Notification"] = "Invalid OTP";
                return View(resetPasswordVM);
            }
            if (DateTime.UtcNow > userOTP.ValidTo)
            {
                TempData["error-Notification"] = "Expired OTP";
                return View(resetPasswordVM);
            }
            TempData["success-Notification"] = "Success OTP";
            return RedirectToAction("NewPassword", "Account", new { area = "Identity", UserId = user.Id });
        }


        [HttpGet]
        public IActionResult NewPassword(string UserId)
        {
            return View(new NewPasswordVM
            {
                ApplicationUserId = UserId
            });
        }

        [HttpPost]
        public async Task<IActionResult> NewPassword(NewPasswordVM newPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(newPasswordVM);
            }
            var user = await _userManager.FindByIdAsync(newPasswordVM.ApplicationUserId);

            if (user == null)
            {
                TempData["error-Notification"] = "Invalid User Name Or Email";
                return View(newPasswordVM);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            await _userManager.ResetPasswordAsync(user,token,newPasswordVM.Password);

            TempData["success-Notification"] = "Change Password Successfully";
            return RedirectToAction("Login", "Account", new { area = "Identity"});
        }


        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account", new { area = "Identity" });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return RedirectToAction(nameof(Login));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Try signing in with an external login
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl ?? "/");
            }

            // If the user cannot log in, try finding them by email
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var name = info.Principal.FindFirstValue(ClaimTypes.Name);
            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    Random rand = new Random();
                    string r = rand.Next(1000, 9999).ToString();
                    // Create a new user if they do not exist
                    user = new ApplicationUser
                    {
                        UserName = $"{name.Replace(" ", "")}{r}",
                        Email = email,
                        Name = name
                    };
                    var createUserResult = await _userManager.CreateAsync(user);
                    if (!createUserResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, "Error creating user.");
                        return RedirectToAction(nameof(Login));
                    }
                }

                // Ensure the external login is linked
                var existingLogins = await _userManager.GetLoginsAsync(user);
                var hasGoogleLogin = existingLogins.Any(l => l.LoginProvider == info.LoginProvider);

                if (!hasGoogleLogin)
                {
                    var addLoginResult = await _userManager.AddLoginAsync(user, info);
                    if (!addLoginResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, "Error linking external login.");
                        return RedirectToAction(nameof(Login));
                    }
                }

                // Sign in the user
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl ?? "/");
            }

            return RedirectToAction(nameof(Login));
        }
    }
}
