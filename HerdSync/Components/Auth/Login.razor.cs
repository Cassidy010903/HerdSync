using BLL.Services;
using BLL.Services.Authorization;
using HerdSync.Authorization;
using HerdSync.Shared.DTO.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace HerdSync.Components.Auth
{
    public partial class Login
    {
        [Inject] IAuthService AuthService { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] LoginSessionStore LoginSessionStore { get; set; }

        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _loading;
        private bool _showPassword;

        private void TogglePasswordVisibility() => _showPassword = !_showPassword;

        private async Task HandleLogin()
        {
            _errorMessage = null;

            if (string.IsNullOrWhiteSpace(_username) || string.IsNullOrWhiteSpace(_password))
            {
                _errorMessage = "Please enter your username and password.";
                return;
            }

            _loading = true;

            var result = await AuthService.LoginAsync(new LoginDTO
            {
                Username = _username,
                Password = _password
            });

            if (!result.Success)
            {
                _errorMessage = result.ErrorMessage;
                _loading = false;
                return;
            }

            var principal = BuildPrincipal(result);
            var token = LoginSessionStore.Store(principal);
            Navigation.NavigateTo($"/auth/complete?token={token}", forceLoad: true);
        }

        private static ClaimsPrincipal BuildPrincipal(AuthResultDTO result)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString()),
                new Claim(ClaimTypes.Name, result.Username),
                new Claim("DisplayName", result.DisplayName),
                new Claim("RoleCode", result.RoleCode),
                new Claim("IsSystemAdmin", result.IsSystemAdmin.ToString().ToLower())
            };

            if (result.FarmId.HasValue)
            {
                claims.Add(new Claim("FarmId", result.FarmId.Value.ToString()));
                claims.Add(new Claim("FarmName", result.FarmName ?? string.Empty));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
    }
}