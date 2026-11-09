using System.Net.Http.Json;

namespace RaceDayMVC.Services
{
    public class AuthenticationServices
    {
        private readonly HttpClient _httpClient;

        public AuthenticationServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Sends registration details to the API.
        public async Task<(bool Success, string Message)> RegisterAsync(
            RegisterData data)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/auth/register", data);

            string message = await response.Content.ReadAsStringAsync();

            return (response.IsSuccessStatusCode, message);
        }

        // Sends login details to the API.
        public async Task<(bool Success, string Message, AuthUser? User)>
            LoginAsync(LoginData data)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/auth/login", data);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                return (false, error, null);
            }

            var user = await response.Content.ReadFromJsonAsync<AuthUser>();

            return (user != null, "Login successful.", user);
        }
    }

    public class RegisterData
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string? Phone { get; set; }
    }

    public class LoginData
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class AuthUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";
    }
}