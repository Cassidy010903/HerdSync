using BLL.Services;
using Kudde.Shared.DTO.Animal;
using Kudde.Shared.DTO.Treatment;
using Microsoft.AspNetCore.Components;

namespace Kudde.Components.Pages.Herd
{
    public partial class Herd
    {
        [Inject] public IAnimalService AnimalService { get; set; } = default!;
        [Inject] public IAnimalObservationService AnimalObservationService { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;
        [Inject] public IAnimalTypeService AnimalTypeService { get; set; } = default!;
        private Dictionary<string, string> _animalTypeNames = new();
        [Inject] public IConditionService ConditionService { get; set; } = default!;
        private List<ConditionDTO> _conditions = new();

        private bool _loading = true;
        private bool _showDialog = false;
        private Guid _editingCowId = Guid.Empty;
        public List<AnimalDTO> HerdList { get; set; } = new();
        private int CowCount;
        private string _searchTerm = string.Empty;
        private string _genderFilter = string.Empty;
        private string _brandedFilter = string.Empty;
        private bool _filterPanelOpen = false;

        // --- Observation state ---
        private bool _showObservationDialog = false;
        private AnimalDTO? _observationTarget;
        private AnimalObservationDTO _observation = new();
        private string _observationError = string.Empty;

        private void ToggleFilterPanel() => _filterPanelOpen = !_filterPanelOpen;
        private void OnGenderFilterChanged(string value) { _genderFilter = value; StateHasChanged(); }
        private void OnBrandedFilterChanged(string value) { _brandedFilter = value; StateHasChanged(); }
        private void OnSearchChanged(string value) { _searchTerm = value; StateHasChanged(); }

        private void ClearFilters()
        {
            _searchTerm = string.Empty;
            _genderFilter = string.Empty;
            _brandedFilter = string.Empty;
            _filterPanelOpen = false;
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

        protected override async Task OnInitializedAsync()
        {
            try
            {
                HerdList = await AnimalService.GetAllAsync();
                var types = await AnimalTypeService.GetAllAsync();
                _animalTypeNames = types.ToDictionary(t => t.AnimalTypeCode, t => t.AnimalTypeName);
                var conditions = await ConditionService.GetAllAsync();
                _conditions = conditions.ToList();
                CowCount = HerdList.Count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                _loading = false;
                StateHasChanged();
            }
        }

        private void OpenDialogAsync()
        {
            _editingCowId = Guid.Empty;
            _showDialog = true;
        }

        private void EditDialog(Guid animalId)
        {
            _editingCowId = animalId;
            _showDialog = true;
        }

        private async Task HandleSubmit(AnimalDTO updatedCow)
        {
            var index = HerdList.FindIndex(c => c.AnimalId == updatedCow.AnimalId);
            if (index >= 0) HerdList[index] = updatedCow;
            else HerdList.Add(updatedCow);
            CowCount = HerdList.Count;
            _showDialog = false;
            StateHasChanged();
        }

        private void HandleCancel()
        {
            _showDialog = false;
            StateHasChanged();
        }

        private void OpenObservationDialog(AnimalDTO animal)
        {
            _observationTarget = animal;
            _observationError = string.Empty;
            _observation = new AnimalObservationDTO
            {
                AnimalId = animal.AnimalId,
                ObservationDate = DateTime.Today
            };
            _showObservationDialog = true;
        }

        private void CloseObservationDialog()
        {
            _showObservationDialog = false;
            _observationTarget = null;
            _observation = new();
            _observationError = string.Empty;
        }

        private async Task SubmitObservation()
        {
            if (string.IsNullOrWhiteSpace(_observation.ConditionCode) &&
                string.IsNullOrWhiteSpace(_observation.Notes) &&
                string.IsNullOrWhiteSpace(_observation.TextValue) &&
                !_observation.NumericValue.HasValue)
            {
                _observationError = "Please fill in at least one observation field.";
                return;
            }

            try
            {
                await AnimalObservationService.CreateAsync(_observation);
                CloseObservationDialog();
            }
            catch (Exception ex)
            {
                _observationError = ex.Message;
            }
        }
    }
}