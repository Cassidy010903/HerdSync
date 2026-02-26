using BLL.Services;
using HerdSync.Shared.DTO.Animal;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HerdSync.Components.Pages.Herd
{
    public partial class Herd
    {
        [Inject] public IAnimalService AnimalService { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;

        private bool _loading = true;
        public List<AnimalDTO> HerdList { get; set; } = new();
        private int CowCount;

        private string _searchTerm = string.Empty;
        private string _genderFilter = string.Empty;
        private string _brandedFilter = string.Empty;
        private bool _genderDropdownOpen = false;
        private bool _brandedDropdownOpen = false;
        private bool _filterPanelOpen = false;

        private void ToggleFilterPanel()
        {
            _filterPanelOpen = !_filterPanelOpen;
        }

        private void OnGenderFilterChanged(string value)
        {
            _genderFilter = value;
            StateHasChanged();
        }

        private void OnBrandedFilterChanged(string value)
        {
            _brandedFilter = value;
            StateHasChanged();
        }

        private void ClearFilters()
        {
            _searchTerm = string.Empty;
            _genderFilter = string.Empty;
            _brandedFilter = string.Empty;
            _filterPanelOpen = false;
            StateHasChanged();
        }

        private void ToggleGenderFilter()
        {
            _genderDropdownOpen = !_genderDropdownOpen;
            _brandedDropdownOpen = false;
        }

        private void ToggleBrandedFilter()
        {
            _brandedDropdownOpen = !_brandedDropdownOpen;
            _genderDropdownOpen = false;
        }

        private void OnSearchChanged(string value)
        {
            _searchTerm = value;
            StateHasChanged();
        }
        private IEnumerable<AnimalDTO> FilteredHerdList => HerdList
            .Where(c =>
                (string.IsNullOrWhiteSpace(_searchTerm) ||
                 (c.DisplayIdentifier?.Contains(_searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                 (c.AnimalTypeCode?.Contains(_searchTerm, StringComparison.OrdinalIgnoreCase) ?? false)) &&
                (string.IsNullOrWhiteSpace(_genderFilter) ||
                 (_genderFilter == "Male" && c.Gender == "M") ||
                 (_genderFilter == "Female" && c.Gender == "F")) &&
                (string.IsNullOrWhiteSpace(_brandedFilter) ||
                 (_brandedFilter == "Yes" && c.IsBranded) ||
                 (_brandedFilter == "No" && !c.IsBranded))
            );

        private string GetGenderIcon(string? gender) => gender switch
        {
            "M" or "Male" => Icons.Material.Filled.Male,
            "F" or "Female" => Icons.Material.Filled.Female,
            _ => Icons.Material.Filled.QuestionMark
        };

        private Color GetGenderColor(string? gender) => gender switch
        {
            "M" or "Male" => Color.Info,
            "F" or "Female" => Color.Secondary,
            _ => Color.Default
        };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _loading = true;
                StateHasChanged();
                HerdList = await AnimalService.GetAllAsync();
                CowCount = HerdList.Count;
                _loading = false;
                StateHasChanged();
            }
        }

        protected override Task OnInitializedAsync() => Task.CompletedTask;

        private async Task OpenDialogAsync()
        {
            var parameters = new DialogParameters { ["HerdList"] = HerdList };
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var dialog = await DialogService.ShowAsync<NewCow>("Add New Cow", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                HerdList = await AnimalService.GetAllAsync();
                CowCount = HerdList.Count;
                StateHasChanged();
            }
        }

        private async Task EditDialog(Guid animalId)
        {
            var parameters = new DialogParameters
            {
                ["ExistingCow"] = animalId,
                ["HerdList"] = HerdList
            };
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var dialog = await DialogService.ShowAsync<NewCow>("Edit Cow", parameters, options);
            var result = await dialog.Result;

            if (result.Data is AnimalDTO updatedCow)
            {
                var index = HerdList.FindIndex(c => c.AnimalId == updatedCow.AnimalId);
                if (index >= 0) HerdList[index] = updatedCow;
                else HerdList.Add(updatedCow);
                CowCount = HerdList.Count;
                StateHasChanged();
            }
        }
    }
}