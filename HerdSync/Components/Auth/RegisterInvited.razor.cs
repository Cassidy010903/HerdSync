using BLL.Services;
using BLL.Services.Authorization;
using HerdSync.Authorization;
using HerdSync.Shared.DTO.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace HerdSync.Components.Auth
{
    public partial class RegisterInvited
    {
        [Inject] IAuthService AuthService { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] LoginSessionStore LoginSessionStore { get; set; }

        private RegisterInvitedDTO _dto = new();
        private string _confirmPassword;
        private string _errorMessage;
        private bool _loading;
        private bool _showPassword;

        [SupplyParameterFromQuery(Name = "code")]
        public string InviteCodeParam { get; set; }

        protected override void OnInitialized()
        {
            if (!string.IsNullOrWhiteSpace(InviteCodeParam))
                _dto.InviteCode = InviteCodeParam.ToUpper();
        }

        private void TogglePasswordVisibility() => _showPassword = !_showPassword;

        private async Task HandleRegister()
        {
            _errorMessage = null;

            if (string.IsNullOrWhiteSpace(_dto.InviteCode))
            {
                _errorMessage = "An invite code is required.";
                return;
            }

            if (string.IsNullOrWhiteSpace(_dto.Username) ||
                string.IsNullOrWhiteSpace(_dto.DisplayName) ||
                string.IsNullOrWhiteSpace(_dto.Password))
            {
                _errorMessage = "Full name, username and password are required.";
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
            _dto.InviteCode = _dto.InviteCode.Trim().ToUpper();

            var result = await AuthService.RegisterInvitedAsync(_dto);

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