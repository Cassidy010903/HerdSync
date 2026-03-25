using BLL.Services;
using BLL.Services.Authorization;
using HerdSync.Shared.DTO.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace HerdSync.Components.Auth
{
    public partial class InviteUsers
    {
        [Inject] IAuthService AuthService { get; set; }
        [Inject] AuthenticationStateProvider AuthStateProvider { get; set; }

        private List<InviteResultDTO> _activeInvites = new();
        private string _selectedRole = "WORK";
        private int _expiryHours = 48;
        private string _newInviteCode;
        private DateTime? _newInviteExpiry;
        private string _errorMessage;
        private string _successMessage;
        private bool _loading;
        private bool _generating;
        private Guid _farmId;

        private readonly Dictionary<string, string> _roles = new()
        {
            { "MNGR", "Farm Manager" },
            { "WORK", "Worker" },
            { "VIEW", "Viewer" }
        };

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var farmIdClaim = authState.User.FindFirst("FarmId")?.Value;

            if (string.IsNullOrEmpty(farmIdClaim))
            {
                _errorMessage = "No farm associated with your account.";
                return;
            }

            _farmId = Guid.Parse(farmIdClaim);
            await LoadInvites();
        }

        private async Task LoadInvites()
        {
            _loading = true;
            _activeInvites = await AuthService.GetActiveInvitesAsync(_farmId);
            _loading = false;
        }

        private async Task GenerateInvite()
        {
            _errorMessage = null;
            _successMessage = null;
            _generating = true;
            _newInviteCode = null;

            var result = await AuthService.CreateInviteAsync(new CreateInviteDTO
            {
                FarmId = _farmId,
                AssignedRoleCode = _selectedRole,
                ExpiryHours = _expiryHours
            });

            if (!result.Success)
            {
                _errorMessage = result.ErrorMessage;
            }
            else
            {
                _newInviteCode = result.InviteCode;
                _newInviteExpiry = result.ExpiresAt;
                _successMessage = "Invite code generated successfully.";
                await LoadInvites();
            }

            _generating = false;
        }
    }
}