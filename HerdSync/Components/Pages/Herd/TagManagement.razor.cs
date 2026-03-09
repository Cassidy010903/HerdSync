using BLL.Services;
using HerdSync.Shared.DTO.Animal;
using Microsoft.AspNetCore.Components;

namespace HerdSync.Components.Pages.Herd
{
    public partial class TagManagement
    {
        [Inject] public IAnimalTagService TagService { get; set; } = default!;
        [Inject] public IAnimalService AnimalService { get; set; } = default!;

        public List<AnimalTagDTO> TagList { get; set; } = new();
        public List<AnimalDTO> HerdList { get; set; } = new();
        private int TagCount;

        private bool _showDialog = false;
        private bool _showDeleteConfirm = false;
        private Guid _editingTagId = Guid.Empty;
        private Guid _deletingTagId = Guid.Empty;

        private string _searchTerm = string.Empty;
        private string _currentFilter = string.Empty;
        private bool _filterPanelOpen = false;

        private void ToggleFilterPanel() => _filterPanelOpen = !_filterPanelOpen;
        private void OnSearchChanged(string value) { _searchTerm = value; StateHasChanged(); }
        private void OnCurrentFilterChanged(string value) { _currentFilter = value; StateHasChanged(); }

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

        private void OpenDialogAsync()
        {
            _editingTagId = Guid.Empty;
            _showDialog = true;
        }

        private void EditDialog(Guid tagId)
        {
            _editingTagId = tagId;
            _showDialog = true;
        }

        private async Task HandleSubmit(AnimalTagDTO updatedTag)
        {
            var index = TagList.FindIndex(t => t.AnimalTagId == updatedTag.AnimalTagId);
            if (index >= 0) TagList[index] = updatedTag;
            else TagList.Add(updatedTag);
            TagCount = TagList.Count;
            _showDialog = false;
            StateHasChanged();
        }

        private void HandleCancel()
        {
            _showDialog = false;
            StateHasChanged();
        }

        private void DeleteTag(Guid tagId)
        {
            _deletingTagId = tagId;
            _showDeleteConfirm = true;
        }

        private async Task ConfirmDelete()
        {
            await TagService.SoftDeleteAsync(_deletingTagId);
            TagList.RemoveAll(t => t.AnimalTagId == _deletingTagId);
            TagCount = TagList.Count;
            _showDeleteConfirm = false;
            StateHasChanged();
        }

        private void CancelDelete()
        {
            _deletingTagId = Guid.Empty;
            _showDeleteConfirm = false;
        }

        private string GetAnimalInfo(Guid animalId)
        {
            var match = HerdList.FirstOrDefault(a => a.AnimalId == animalId);
            return match?.DisplayIdentifier ?? "Unknown";
        }
    }
}