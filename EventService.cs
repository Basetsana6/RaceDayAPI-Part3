using System.Net.Http;
using System.Threading.Tasks;

namespace RaceDayMVC.Services
{
    public class EventService
    {
        private readonly HttpClient _httpClient;

        public EventService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetEventsAsync()
        {
            var response = await _httpClient.GetAsync("api/events");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}