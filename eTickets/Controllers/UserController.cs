using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eTickets.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly SignInManager<ApplicationUser> _signInManger;
        private readonly eTicketContext _context;

        public UserController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            eTicketContext context)
        {
            _userManger = userManager;
            _signInManger = signInManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginVM();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View(login);

            var user = await _userManger.FindByEmailAsync(login.EmailAddress);

            if(user != null) 
            {
                var passCheck = await _userManger.CheckPasswordAsync(user,login.Password);
                if (passCheck) 
                {
                    var result = await _signInManger.PasswordSignInAsync(user, login.Password,false, false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
                    TempData["Error"] = "Wrong User. Please try again!";
                    return View(login);
            }
            TempData["Error"] = "Wrong User. Please try again!";
            return View(login);
        }

        public IActionResult Register()
        {
            var response = new RegisterVM();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM reg)
        {
            if(!ModelState.IsValid) return View(reg);
            var user = await _userManger.FindByEmailAsync(reg.EmailAddress);
            if(user != null)
            {
                TempData["Error"] = "This email is already in use";
                return View(reg);
            }
            var newUser = new ApplicationUser()
            {
                FullName = reg.FullName,
                Email = reg.EmailAddress,
                UserName = reg.EmailAddress
            };
            var newUserResponse = await _userManger.CreateAsync(newUser, reg.Password);
            if (newUserResponse.Succeeded)
            {
                await _userManger.AddToRoleAsync(newUser, UserRoles.User);
            }
            return View("RegisterCompleted");

        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManger.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }
        //    [HttpPost]
        //    public async Task<IActionResult> Login(Login user, string returnUrl)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var search = await _service.LoginAsync(user, returnUrl);
        //            Claim c1 = new Claim(ClaimTypes.Name, search.Name);
        //            Claim c2 = new Claim(ClaimTypes.Email, search.Email);
        //            ClaimsIdentity claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        //            claimsIdentity.AddClaim(c1);
        //            claimsIdentity.AddClaim(c2);
        //            foreach (var role in search.Roles)
        //            {
        //                Claim c3 = new Claim(ClaimTypes.Role, role.RoleName);
        //                claimsIdentity.AddClaim(c3);
        //            }
        //            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
        //            claimsPrincipal.AddIdentity(claimsIdentity);
        //            await HttpContext.SignInAsync(claimsPrincipal);
        //            if (returnUrl != null)
        //            {
        //                return LocalRedirect(returnUrl);
        //            }
        //        }
        //        ModelState.AddModelError("", "Email or Password Is Incorrect");
        //        return View(user);
        //    }
        //    public async Task<IActionResult> Register(User user)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            await _service.RegisterAsync(user);
        //            return RedirectToAction("Login", "User");
        //        }
        //        return View(user);
        //    }
    }
}
