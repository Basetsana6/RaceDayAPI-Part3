using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ResultsService _resultsService;

        public ResultsController(ResultsService resultsService)
        {
            _resultsService = resultsService;
        }

        // Display race results.
        public async Task<IActionResult> Index()
        {
            try
            {
                var results = await _resultsService.GetResultsAsync();
                return View(results);
            }
            catch
            {
                ViewBag.Error = "Unable to retrieve race results.";
                return View(new List<RaceResultViewModel>());
            }
        }

        // Display the result entry form.
        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return Forbid();

            return View();
        }

        // Save a new race result.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RaceResultViewModel result)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return Forbid();

            if (!ModelState.IsValid)
                return View(result);

            var response = await _resultsService.CreateResultAsync(result);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Race result recorded successfully.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("",
                "Unable to save the result. Check that the enrolment ID exists.");

            return View(result);
        }
    }
}