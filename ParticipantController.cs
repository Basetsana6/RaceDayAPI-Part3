using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly EventService _eventService;
        private readonly ParticipantService _participantService;

        public ParticipantController(
            EventService eventService,
            ParticipantService participantService)
        {
            _eventService = eventService;
            _participantService = participantService;
        }

        private bool IsParticipant()
        {
            return HttpContext.Session.GetString("UserRole") == "Participant";
        }

        private int GetParticipantId()
        {
            return HttpContext.Session.GetInt32("UserId") ?? 0;
        }

        public IActionResult Index()
        {
            if (!IsParticipant())
                return RedirectToAction("Login", "Account");

            ViewBag.ParticipantName = HttpContext.Session.GetString("UserName");
            return View();
        }

        public async Task<IActionResult> Events()
        {
            if (!IsParticipant())
                return RedirectToAction("Login", "Account");

            var events = await _eventService.GetEventsAsync();
            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Register(int id)
        {
            if (!IsParticipant())
                return RedirectToAction("Login", "Account");

            var raceEvent = await _eventService.GetEventAsync(id);

            if (raceEvent == null)
                return NotFound();

            var categories = await _participantService.GetCategoriesAsync(id);

            ViewBag.Event = raceEvent;
            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(int eventId, int categoryId)
        {
            if (!IsParticipant())
                return RedirectToAction("Login", "Account");

            int participantId = GetParticipantId();

            if (participantId == 0)
                return RedirectToAction("Login", "Account");

            var enrolment = new EnrolmentData
            {
                ParticipantId = participantId,
                EventId = eventId,
                CategoryId = categoryId
            };

            var response = await _participantService.RegisterAsync(enrolment);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "You have successfully registered for the event.";
                return RedirectToAction(nameof(MyEvents));
            }

            TempData["Error"] = await response.Content.ReadAsStringAsync();
            return RedirectToAction(nameof(Register), new { id = eventId });
        }

        public async Task<IActionResult> MyEvents()
        {
            if (!IsParticipant())
                return RedirectToAction("Login", "Account");

            int participantId = GetParticipantId();

            if (participantId == 0)
                return RedirectToAction("Login", "Account");

            var enrolments = await _participantService.GetMyEventsAsync(participantId);
            var events = await _eventService.GetEventsAsync();

            ViewBag.Events = events;
            return View(enrolments);
        }
    }
}