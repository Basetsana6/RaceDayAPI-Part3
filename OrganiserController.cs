using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class OrganiserController : Controller
    {
        private readonly ParticipantService _participantService;

        public OrganiserController(ParticipantService participantService)
        {
            _participantService = participantService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return StatusCode(403);

            ViewBag.OrganiserName =
                HttpContext.Session.GetString("UserName");

            return View();
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return StatusCode(403);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryData category)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return StatusCode(403);

            var response = await _participantService.CreateCategoryAsync(category);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Race category created successfully.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Unable to create category. Check the event ID.");
            return View(category);
        }
    }
}