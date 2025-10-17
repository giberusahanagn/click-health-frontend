using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ClickHealthFrontend.Auth
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _cachedUser = new ClaimsPrincipal(new ClaimsIdentity());
        private string _userEmail = "";
        private string _userRole = "";

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(100);
            return new AuthenticationState(_cachedUser);
        }

        public async Task LoginAsync(string email, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, email)
            };

            var identity = new ClaimsIdentity(claims, "OtpAuth");
            _cachedUser = new ClaimsPrincipal(identity);
            _userEmail = email;
            _userRole = role;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task LogoutAsync()
        {
            _cachedUser = new ClaimsPrincipal(new ClaimsIdentity());
            _userEmail = "";
            _userRole = "";
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
