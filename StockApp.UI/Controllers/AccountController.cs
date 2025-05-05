using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockApp.Core.Domain.IdentityEntities;
using StockApp.Core.DTO;

namespace StockApp.UI.Controllers
{
    [Route("[controller]")]
    //[AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Route("[action]")]
        [HttpGet]
        [Authorize(Policy = "NotAuthenticated")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        [Authorize(Policy = "NotAuthenticated")]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return View(userRegister);
            }

            ApplicationUser user = new ApplicationUser()
            {
                UserName = userRegister.Username,
                Email = userRegister.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Explore", "Stocks");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage).ToList();
                return View(userRegister);
            }
        }

        [Route("[action]")]
        [HttpGet]
        [Authorize(Policy = "NotAuthenticated")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        [Authorize(Policy = "NotAuthenticated")]
        public async Task<IActionResult> Login(UserLogin userLogin, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return View(userLogin);
            }

            var user = await _userManager.FindByEmailAsync(userLogin.Email);

            if (user == null)
            {
                ModelState.AddModelError("Login", "Invalid email");
                return View(userLogin);
            }

            var result = await _signInManager.PasswordSignInAsync(user, userLogin.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }
                return RedirectToAction("Explore", "Stocks");
            }
            else
            {
                ModelState.AddModelError("Login", "Invalid login attempt.");
                return View(userLogin);
            }
        }

        [Route("[action]")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            return Json(false);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> IsUsernameAlreadyRegistered(string username)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Json(true);
            }
            return Json(false);
        }
    }
}
