using System.Net.Http.Headers;

namespace RaceDayMVC.Services
{
    public class ImageService
    {
        private readonly HttpClient _httpClient;

        public ImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> UploadImageAsync(IFormFile image)
        {
            using var content = new MultipartFormDataContent();
            await using var stream = image.OpenReadStream();

            using var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType =
                new MediaTypeHeaderValue(image.ContentType);

            content.Add(fileContent, "file", image.FileName);

            var response = await _httpClient.PostAsync(
                "api/images/upload", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<ImageUploadResponse>();

            return result?.ImageUrl;
        }

        private class ImageUploadResponse
        {
            public string? ImageUrl { get; set; }
        }
    }
}