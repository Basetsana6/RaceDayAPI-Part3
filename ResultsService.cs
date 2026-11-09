using System.Net.Http.Json;

namespace RaceDayMVC.Services
{
    public class ResultsService
    {
        private readonly HttpClient _httpClient;

        public ResultsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RaceResultViewModel>> GetResultsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<RaceResultViewModel>>(
                "api/results") ?? new List<RaceResultViewModel>();
        }

        public async Task<HttpResponseMessage> CreateResultAsync(
            RaceResultViewModel result)
        {
            return await _httpClient.PostAsJsonAsync("api/results", result);
        }
    }

    public class RaceResultViewModel
    {
        public int ResultId { get; set; }
        public int EnrolmentId { get; set; }
        public TimeSpan? FinishTime { get; set; }
        public int? Position { get; set; }
        public string ResultStatus { get; set; } = "Finished";
        public DateTime RecordedAt { get; set; }
    }
}