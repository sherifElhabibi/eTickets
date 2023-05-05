using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eTickets.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login user,string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var search = await _service.LoginAsync(user, returnUrl);
                Claim c1 = new Claim(ClaimTypes.Name, search.Name);
                Claim c2 = new Claim(ClaimTypes.Email, search.Email);
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                claimsIdentity.AddClaim(c1);
                claimsIdentity.AddClaim(c2);
                foreach (var role in  search.Roles)
                {
                    Claim c3 = new Claim(ClaimTypes.Role,role.RoleName);
                    claimsIdentity.AddClaim(c3);
                }
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
                claimsPrincipal.AddIdentity(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                if(returnUrl != null)
                {
                    return LocalRedirect(returnUrl);
                }
            }
                ModelState.AddModelError("", "Email or Password Is Incorrect");
                return View(user);
        }
        public async Task<IActionResult> Register(User user)
        {
            if(ModelState.IsValid)
            {
               await _service.RegisterAsync(user);
               return RedirectToAction("Login","User");
            }
            return View(user);
        }
    }
}
