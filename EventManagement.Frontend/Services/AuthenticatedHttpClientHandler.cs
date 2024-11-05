using System.Net.Http.Headers;

namespace EventManagement.Frontend.Services
{
    public class AuthenticatedHttpClientHandler : DelegatingHandler
    {
        private readonly AuthService _authService;

        public AuthenticatedHttpClientHandler(AuthService authService)
        {
            _authService = authService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _authService.GetToken(); 
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
