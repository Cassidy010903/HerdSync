using BLL.Services;
using BLL.Services.Authorization;
using HerdSync.Authorization;
using HerdSync.Shared.DTO.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace HerdSync.Components.Auth
{
    public partial class RegisterOwner
    {
        [Inject] IAuthService AuthService { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] LoginSessionStore LoginSessionStore { get; set; }
        [Inject] ActivityFeedService ActivityFeed { get; set; } = default!;

        private RegisterOwnerDTO _dto = new();
        private string _confirmPassword;
        private string _errorMessage;
        private bool _loading;
        private bool _showPassword;

        private void TogglePasswordVisibility() => _showPassword = !_showPassword;

        private async Task HandleRegister()
        {
            _errorMessage = null;

            if (string.IsNullOrWhiteSpace(_dto.FarmName) ||
                string.IsNullOrWhiteSpace(_dto.Username) ||
                string.IsNullOrWhiteSpace(_dto.DisplayName) ||
                string.IsNullOrWhiteSpace(_dto.Password))
            {
                _errorMessage = "Farm name, full name, username and password are required.";
                return;
            }

            if (_dto.Password != _confirmPassword)
            {
                _errorMessage = "Passwords do not match.";
                return;
            }

            if (_dto.Password.Length < 8)
            {
                _errorMessage = "Password must be at least 8 characters.";
                return;
            }

            _loading = true;

            var result = await AuthService.RegisterOwnerAsync(_dto);

            if (!result.Success)
            {
                _errorMessage = result.ErrorMessage;
                _loading = false;
                return;
            }

            var principal = BuildPrincipal(result);
            var token = LoginSessionStore.Store(principal);
            ActivityFeed.Add(new ActivityFeedService.ActivityEntry
            {
                Text = $"New user joined — {result.DisplayName}",
                Timestamp = DateTime.Now,
                Color = "#3a7fa8",
                Type = ActivityFeedService.ActivityType.NewUser
            });
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
                new Claim("IsSystemAdmin", result.IsSystemAdmin.ToString().ToLower()),
                new Claim("FarmId", result.FarmId.Value.ToString()),
                new Claim("FarmName", result.FarmName ?? string.Empty)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
    }
}