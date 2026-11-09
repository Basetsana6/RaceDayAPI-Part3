using System.Net.Http.Json;
using RaceDayAPI.Models;

namespace RaceDayMVC.Services
{
    public class ParticipantService
    {
        private readonly HttpClient _httpClient;

        public ParticipantService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> CreateCategoryAsync(CategoryData category)
        {
            return await _httpClient.PostAsJsonAsync("api/categories", category);
        }

        public async Task<List<CategoryData>> GetCategoriesAsync(int eventId)
        {
            return await _httpClient.GetFromJsonAsync<List<CategoryData>>(
                $"api/categories/event/{eventId}") ?? new List<CategoryData>();
        }

        public async Task<List<EnrolmentData>> GetMyEventsAsync(int participantId)
        {
            return await _httpClient.GetFromJsonAsync<List<EnrolmentData>>(
                $"api/enrolments/participant/{participantId}")
                ?? new List<EnrolmentData>();
        }

        public async Task<HttpResponseMessage> RegisterAsync(EnrolmentData enrolment)
        {
            return await _httpClient.PostAsJsonAsync("api/enrolments", enrolment);
        }
    }

    public class CategoryData
    {
        public int CategoryId { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; } = "";
        public double DistanceKm { get; set; }
        public int MaxParticipants { get; set; }
        public decimal EntryFee { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }

    public class EnrolmentData
    {
        public int EnrolmentId { get; set; }
        public int ParticipantId { get; set; }
        public int EventId { get; set; }
        public int CategoryId { get; set; }
        public DateTime EnrolmentDate { get; set; }
        public string Status { get; set; } = "Pending";
        public int? BibNumber { get; set; }
    }
}