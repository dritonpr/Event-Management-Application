using EventManagement.Common.Dto;

namespace EventManagement.Client.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Register(UserRegisterDto registerDto)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/register", registerDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> Login(UserLoginDto loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", loginDto);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                return result?["Token"];
            }
            return null;
        }
        public async Task<bool> Logins(string userName, string password)
        {
            //var response = await _httpClient.PostAsJsonAsync("auth/login", loginDto);
            //if (response.IsSuccessStatusCode)
            //{
            //    var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            //    return result?["Token"];
            //}
            return true;
        }
    }
}
