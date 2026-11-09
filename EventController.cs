using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventService _eventService;
        private readonly ImageService _imageService;
        public EventsController(EventService eventService, ImageService imageService)
        {
            _eventService = eventService;
            _imageService = imageService;
        }
       

        // Displays all available events.
        public async Task<IActionResult> Index()
        {
            try
            {
                var events = await _eventService.GetEventsAsync();
                return View(events);
            }
            catch
            {
                ViewBag.Error = "Unable to retrieve events.";
                return View(new List<RaceEventViewModel>());
            }
        }

        // Displays event details.
        public async Task<IActionResult> Details(int id)
        {
            var raceEvent = await _eventService.GetEventAsync(id);

            if (raceEvent == null)
                return NotFound();

            return View(raceEvent);
        }

        // Displays the create event form.
        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return Forbid();

            return View();
        }

        // Creates a new event.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RaceEventViewModel raceEvent)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return Forbid();

            if (!ModelState.IsValid)
                return View(raceEvent);

            raceEvent.OrganiserId =
                HttpContext.Session.GetInt32("UserId") ?? 0;

            if (raceEvent.OrganiserId == 0)
                return Forbid();

            var response = await _eventService.CreateEventAsync(raceEvent);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Unable to create event.";
                return View(raceEvent);
            }

            TempData["Success"] = "Event created successfully.";

            return RedirectToAction("Index");
        }

        // Displays the edit event form.
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return Forbid();

            var raceEvent = await _eventService.GetEventAsync(id);

            if (raceEvent == null)
                return NotFound();

            return View(raceEvent);
        }

        // Updates an existing event.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, RaceEventViewModel raceEvent)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return Forbid();

            if (id != raceEvent.EventId)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(raceEvent);

            var response = await _eventService.UpdateEventAsync(id, raceEvent);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Unable to update event.";
                return View(raceEvent);
            }

            TempData["Success"] = "Event updated successfully.";

            return RedirectToAction("Index");
        }

        // Deletes an event.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return  StatusCode(403); // Forbidden

            var response = await _eventService.DeleteEventAsync(id);

            if (!response.IsSuccessStatusCode)
                TempData["Error"] = "Unable to delete event.";

            return RedirectToAction("Index");
        }
    }
}