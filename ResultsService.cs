using System.Net.Http;
using System.Threading.Tasks;

namespace RaceDayMVC.Services
{
    public class ResultsService
    {
        private readonly HttpClient _httpClient;

        public ResultsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetResultsAsync()
        {
            var response = await _httpClient.GetAsync("api/results");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}