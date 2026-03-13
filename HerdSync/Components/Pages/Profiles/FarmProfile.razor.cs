using BLL.Services;
using HerdSync.Shared.DTO.Authentication;
using HerdSync.Shared.DTO.Farm;
using Microsoft.AspNetCore.Components;

namespace HerdSync.Components.Pages.Profiles
{
    public partial class FarmProfile
    {
        [Inject] public IFarmService FarmService { get; set; } = default!;
        [Inject] public IFarmUserService FarmUserService { get; set; } = default!;
        [Inject] public IFarmActivityService FarmActivityService { get; set; } = default!;
        [Inject] public IFarmActivityTypeService FarmActivityTypeService { get; set; } = default!;
        [Inject] public IUserAccountService UserAccountService { get; set; } = default!;
        [Inject] public IUserRoleService UserRoleService { get; set; } = default!;

        private bool _loading = true;
        private FarmDTO? _farm;
        private FarmDTO _farmEdit = new();
        private bool _editingFarm = false;
        private string _farmError = string.Empty;
        private List<FarmUserDTO> _farmUsers = new();
        private List<UserAccountDTO> _users = new();
        private List<UserRoleDTO> _roles = new();
        private List<FarmActivityDTO> _activities = new();
        private List<FarmActivityTypeDTO> _activityTypes = new();
        private bool _showAddUserDialog = false;
        private string _newUserUsername = string.Empty;
        private string _newUserRoleCode = string.Empty;
        private string _addUserError = string.Empty;
        private bool _showRemoveUserDialog = false;
        private FarmUserDTO? _removingUser;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var farms = await FarmService.GetAllAsync();
                _farm = farms.FirstOrDefault();
                if (_farm != null)
                    _farmEdit = new FarmDTO { FarmId = _farm.FarmId, FarmName = _farm.FarmName, OwnerUserId = _farm.OwnerUserId };
                _farmUsers = (await FarmUserService.GetAllAsync()).ToList();
                _users = (await UserAccountService.GetAllAsync()).ToList();
                _roles = (await UserRoleService.GetAllAsync()).ToList();
                _activities = (await FarmActivityService.GetAllAsync()).OrderByDescending(a => a.ActivityDate).ToList();
                _activityTypes = (await FarmActivityTypeService.GetAllAsync()).ToList();
            }
            catch (Exception ex) { Console.WriteLine($"FarmProfile error: {ex}"); }
            finally { _loading = false; }
        }

        private async Task SaveFarm()
        {
            _farmError = string.Empty;
            if (string.IsNullOrWhiteSpace(_farmEdit.FarmName)) { _farmError = "Farm name is required."; return; }
            try
            {
                await FarmService.UpdateAsync(_farmEdit);
                _farm = _farmEdit;
                _farmEdit = new FarmDTO { FarmId = _farm.FarmId, FarmName = _farm.FarmName, OwnerUserId = _farm.OwnerUserId };
                _editingFarm = false;
            }
            catch (Exception ex) { _farmError = $"Failed to save: {ex.Message}"; }
        }

        private void OpenAddUserDialog()
        {
            _newUserUsername = string.Empty;
            _newUserRoleCode = string.Empty;
            _addUserError = string.Empty;
            _showAddUserDialog = true;
        }

        private async Task AddUser()
        {
            _addUserError = string.Empty;
            if (string.IsNullOrWhiteSpace(_newUserUsername)) { _addUserError = "Username is required."; return; }
            if (string.IsNullOrWhiteSpace(_newUserRoleCode)) { _addUserError = "Role is required."; return; }
            var user = _users.FirstOrDefault(u => u.Username.Equals(_newUserUsername, StringComparison.OrdinalIgnoreCase));
            if (user == null) { _addUserError = "User not found."; return; }
            try
            {
                var newFarmUser = new FarmUserDTO { FarmUserId = Guid.NewGuid(), FarmId = _farm!.FarmId, UserId = user.UserId, RoleCode = _newUserRoleCode, IsActive = true };
                await FarmUserService.CreateAsync(newFarmUser);
                _farmUsers.Add(newFarmUser);
                _showAddUserDialog = false;
            }
            catch (Exception ex) { _addUserError = $"Failed to add user: {ex.Message}"; }
        }

        private void ConfirmRemoveUser(FarmUserDTO farmUser)
        {
            _removingUser = farmUser;
            _showRemoveUserDialog = true;
        }

        private async Task RemoveUser()
        {
            if (_removingUser == null) return;
            try
            {
                await FarmUserService.SoftDeleteAsync(_removingUser.FarmUserId);
                _farmUsers.Remove(_removingUser);
                _showRemoveUserDialog = false;
            }
            catch (Exception ex) { Console.WriteLine($"Remove user error: {ex}"); }
        }
    }
}