using System.Net.Http;

namespace RaceDayMVC.Services
{
    public class AuthenticationServices
    {
        private readonly HttpClient _httpClient;

        public AuthenticationServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}