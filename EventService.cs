using System.Net.Http.Json;

namespace RaceDayMVC.Services
{
    // Handles event communication with RaceDayAPI.
    public class EventService
    {
        private readonly HttpClient _httpClient;

        public EventService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Retrieves all race events.
        public async Task<List<RaceEventViewModel>> GetEventsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<RaceEventViewModel>>(
                "api/events") ?? new List<RaceEventViewModel>();
        }

        // Retrieves one event.
        public async Task<RaceEventViewModel?> GetEventAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<RaceEventViewModel>(
                $"api/events/{id}");
        }

        // Creates a new event.
        public async Task<HttpResponseMessage> CreateEventAsync(
            RaceEventViewModel raceEvent)
        {
            return await _httpClient.PostAsJsonAsync(
                "api/events", raceEvent);
        }

        // Updates an existing event.
        public async Task<HttpResponseMessage> UpdateEventAsync(
            int id, RaceEventViewModel raceEvent)
        {
            return await _httpClient.PutAsJsonAsync(
                $"api/events/{id}", raceEvent);
        }

        // Deletes an event.
        public async Task<HttpResponseMessage> DeleteEventAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/events/{id}");
        }
    }

    // Represents an event in the MVC application.
    public class RaceEventViewModel
    {
        public int EventId { get; set; }
        public int OrganiserId { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = "";
        public string EventType { get; set; } = "Running";
        public string Status { get; set; } = "Upcoming";
        public DateTime CreatedAt { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}