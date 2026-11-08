using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class ResultsController : Controller
    {
        private readonly RaceDayApiService _apiService;

        public ResultsController(RaceDayApiService apiService)
        {
            _apiService = apiService;
        }

        // Display results retrieved from RaceDayAPI.
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.ApiResponse =
                    await _apiService.GetResultsAsync();

                ViewBag.Error = null;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }
    }
}