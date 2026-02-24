using BLL.Services;
using HerdSync.Shared.DTO.Animal;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HerdSync.Components.Pages.HerdManagement
{
    public partial class NewCow
    {
        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; } = default!;

        [Parameter]
        public Guid ExistingCow { get; set; }

        [Parameter]
        public List<AnimalDTO> HerdList { get; set; } = new();

        [Inject]
        private IAnimalService AnimalService { get; set; } = default!;
        [Inject]
        private IAnimalTypeService AnimalTypeService { get; set; } = default!;

        [Inject]
        private ISnackbar Snackbar { get; set; } = default!;

        public AnimalDTO species = new();
        public List<AnimalDTO> staticParentList { get; set; } = new();
        public bool IsEditMode => ExistingCow != Guid.Empty;
        List<AnimalTypeDTO> animalTypes = new List<AnimalTypeDTO>();
        private AnimalDTO? _selectedMother;
        private AnimalDTO? SelectedMother
        {
            get => _selectedMother;
            set { _selectedMother = value; species.MotherAnimalId = value?.AnimalId; }
        }

        private AnimalDTO? _selectedFather;
        private AnimalDTO? SelectedFather
        {
            get => _selectedFather;
            set { _selectedFather = value; species.FatherAnimalId = value?.AnimalId; }
        }

        private async Task Submit()
        {
            try
            {
                if (IsEditMode)
                {
                    await AnimalService.UpdateAsync(species);
                    MudDialog.Close(DialogResult.Ok(species));
                }
                else
                {
                    await AnimalService.CreateAsync(species);
                    MudDialog.Close(DialogResult.Ok(true));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving animal: {ex.Message}");
                Snackbar.Add("Failed to save cow data.", MudBlazor.Severity.Error);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            var types = await AnimalTypeService.GetAllAsync();
            animalTypes = types.ToList();

            staticParentList = HerdList;

            if (IsEditMode)
            {
                species = HerdList.FirstOrDefault(c => c.AnimalId == ExistingCow) ?? new AnimalDTO();
                _selectedMother = HerdList.FirstOrDefault(c => c.AnimalId == species.MotherAnimalId);
                _selectedFather = HerdList.FirstOrDefault(c => c.AnimalId == species.FatherAnimalId);
            }
        }

        private void Cancel() => MudDialog.Cancel();

        private async Task<IEnumerable<AnimalDTO>> Search(string value, CancellationToken token)
        {
            await Task.Delay(5, token);
            if (string.IsNullOrEmpty(value))
                return staticParentList;

            return staticParentList.Where(x => x.DisplayIdentifier.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}