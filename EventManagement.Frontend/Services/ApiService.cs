using System.Net.Http.Headers;

namespace EventManagement.Frontend.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void SetBearerToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _httpClient.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return await _httpClient.PostAsync(url, content);
        }
        public async Task<HttpResponseMessage> PutAsync(string url, HttpContent content)
        {
            return await _httpClient.PutAsync(url, content);
        }
    }

}
