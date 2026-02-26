using BLL.Services;
using HerdSync.Shared.DTO.Animal;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HerdSync.Components.Pages.Herd
{
    public partial class TagManagement
    {
        [Inject] public IAnimalTagService TagService { get; set; } = default!;
        [Inject] public IAnimalService AnimalService { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;

        public List<AnimalTagDTO> TagList { get; set; } = new();
        public List<AnimalDTO> HerdList { get; set; } = new();
        private int TagCount;

        private string _searchTerm = string.Empty;
        private string _currentFilter = string.Empty;
        private bool _filterPanelOpen = false;

        private void ToggleFilterPanel() => _filterPanelOpen = !_filterPanelOpen;

        private void OnSearchChanged(string value)
        {
            _searchTerm = value;
            StateHasChanged();
        }

        private void OnCurrentFilterChanged(string value)
        {
            _currentFilter = value;
            StateHasChanged();
        }

        private void ClearFilters()
        {
            _searchTerm = string.Empty;
            _currentFilter = string.Empty;
            _filterPanelOpen = false;
            StateHasChanged();
        }

        private IEnumerable<AnimalTagDTO> FilteredTagList => TagList
            .Where(t =>
                (string.IsNullOrWhiteSpace(_searchTerm) ||
                 (t.RFIDTagCode?.Contains(_searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                 GetAnimalInfo(t.AnimalId).Contains(_searchTerm, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(_currentFilter) ||
                 (_currentFilter == "Yes" && t.IsCurrent) ||
                 (_currentFilter == "No" && !t.IsCurrent))
            );

        protected override async Task OnInitializedAsync()
        {
            TagList = (await TagService.GetAllAsync()).ToList();
            HerdList = await AnimalService.GetAllAsync();
            TagCount = TagList.Count;
        }

        private async Task OpenDialogAsync()
        {
            var parameters = new DialogParameters { ["HerdList"] = HerdList };
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var dialog = await DialogService.ShowAsync<NewTagDialog>("Add New Tag", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                TagList = (await TagService.GetAllAsync()).ToList();
                TagCount = TagList.Count;
                StateHasChanged();
            }
        }

        private async Task EditDialog(Guid tagId)
        {
            var parameters = new DialogParameters
            {
                ["ExistingTag"] = tagId,
                ["TagList"] = TagList,
                ["HerdList"] = HerdList
            };
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var dialog = await DialogService.ShowAsync<NewTagDialog>("Edit Tag", parameters, options);
            var result = await dialog.Result;

            if (result.Data is AnimalTagDTO updatedTag)
            {
                var index = TagList.FindIndex(t => t.AnimalTagId == updatedTag.AnimalTagId);
                if (index >= 0) TagList[index] = updatedTag;
                else TagList.Add(updatedTag);
                TagCount = TagList.Count;
                StateHasChanged();
            }
        }

        private async Task DeleteTag(Guid tagId)
        {
            bool? confirmed = await DialogService.ShowMessageBox(
                "Delete Tag",
                "Are you sure you want to delete this tag? This action cannot be undone.",
                yesText: "Delete",
                cancelText: "Cancel");

            if (confirmed == true)
            {
                await TagService.SoftDeleteAsync(tagId);
                TagList.RemoveAll(t => t.AnimalTagId == tagId);
                TagCount = TagList.Count;
                StateHasChanged();
            }
        }

        private string GetAnimalInfo(Guid animalId)
        {
            var match = HerdList.FirstOrDefault(a => a.AnimalId == animalId);
            return match?.DisplayIdentifier ?? "Unknown";
        }
    }
}