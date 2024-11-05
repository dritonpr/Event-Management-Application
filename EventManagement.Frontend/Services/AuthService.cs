using EventManagement.Frontend.Model;
using EventManagement.Frontend.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace EventManagement.Frontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private const string TokenKey = "authToken";
        private const string Username = "";


        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> Register(UserRegisterDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/register", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Login(UserLoginDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", request);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TokenResponse>(jsonString);
                string token = result?.token;
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", Username, request.Username);

                return true;
            }
            return false;
        }

        public async Task Logout()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        }

        public async Task<string> GetToken()
        {
            try
            {
                return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }
        public async Task<bool> IsAuthenticated()
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
            return !string.IsNullOrEmpty(token);
        }
        public async Task<string> GetUsername()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", Username);
        }
    }
}
