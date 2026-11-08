using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthenticationServices _authService;

        public AccountController(AuthenticationServices authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _authService.LoginAsync(
                new LoginData
                {
                    Email = email,
                    Password = password
                });

            if (!result.Success || result.User == null)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            HttpContext.Session.SetInt32("UserId", result.User.UserId);
            HttpContext.Session.SetString("UserName", result.User.FirstName);
            HttpContext.Session.SetString("UserRole", result.User.Role);
            HttpContext.Session.SetString("UserEmail", result.User.Email);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(
            string firstName,
            string lastName,
            string email,
            string password,
            string? phone)
        {
            var result = await _authService.RegisterAsync(
                new RegisterData
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password,
                    Phone = phone
                });

            if (!result.Success)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            TempData["Success"] =
                "Registration successful. Please log in.";

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}