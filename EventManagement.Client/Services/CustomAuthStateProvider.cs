using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace EventManagement.Client.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_anonymous));
        }

        public void NotifyUserAuthentication(string token)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, token) }; // Add specific claims as needed
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
