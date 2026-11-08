using System.Net.Http;
using System.Threading.Tasks;

namespace RaceDayMVC.Services
{
    public class RaceDayApiService
    {
        private readonly HttpClient _httpClient;

        public RaceDayApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Retrieve all race results from the API.
        public async Task<string> GetResultsAsync()
        {
            var response = await _httpClient.GetAsync("api/results");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}