using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo.Web.Clients.Inerfaces;
using Todo.Web.Clients.Models;
using Todo.Web.Models;

namespace Todo.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserClient _userClient;

        public LoginController(IUserClient userClient)
        {
            _userClient = userClient;
        }

        /* ---------- LOGIN ---------- */

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Name,Password")] LoginViewModel model)
        {
            var userId = await _userClient.ValidatePassword(new ValidatePasswordInputModel
            {
                Name = model.Name!,
                Password = model.Password!
            });

            if (userId is null || userId <= 0)
            {
                ModelState.AddModelError(nameof(model.Password), "Invalid password");
                return View(model);
            }

            var user = await _userClient.GetById(userId);
            if (user is null)
            {
                ModelState.AddModelError(nameof(model.Password), "Invalid password");
                return View(model);
            }

            await SignInAsync(user.Id, user.Name!, user.IsAdmin);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        /* ---------- REGISTER ---------- */

        [HttpGet]
        public IActionResult Register() => View(new RegisterViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Password != model.RepeatPassword)
            {
                ModelState.TryAddModelError(nameof(model.RepeatPassword), "Passwords don't match");
                return View(model);
            }

            try
            {
                var userId = await _userClient.CreateUser(new CreateUserInputModel
                {
                    Name = model.Name!,
                    Password = model.Password!,
                    IsAdmin = false
                });

                await SignInAsync(userId, model.Name!, false);
            }
            catch (Exception ex)
            {
                ModelState.TryAddModelError(string.Empty, ex.Message);
                return View(model);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        /* ---------- LOGOUT ---------- */

        [HttpGet, Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

        /* ---------- HELPERS ---------- */

        private async Task SignInAsync(int? userId, string userName, bool isAdmin)
        {
            var identity = new ClaimsIdentity(
                CookieAuthenticationDefaults.AuthenticationScheme,
                "name", "role");

            identity.AddClaim(new Claim("name", userName));
            identity.AddClaim(new Claim("userId", userId?.ToString() ?? string.Empty));
            identity.AddClaim(new Claim("role", isAdmin ? "Administrator" : "User"));

            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
        }
    }
}
