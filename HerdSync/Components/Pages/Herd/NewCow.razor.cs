using BLL.Services;
using HerdSync.Shared.DTO.Animal;
using Microsoft.AspNetCore.Components;

namespace HerdSync.Components.Pages.Herd
{
    public partial class NewCow
    {
        [Parameter] public Guid ExistingCow { get; set; }
        [Parameter] public List<AnimalDTO> HerdList { get; set; } = new();
        [Parameter] public EventCallback<AnimalDTO> OnSubmit { get; set; }
        [Parameter] public EventCallback OnCancel { get; set; }
        [Inject] private IAnimalService AnimalService { get; set; } = default!;
        [Inject] private IAnimalTypeService AnimalTypeService { get; set; } = default!;
        [Inject] private IAnimalTagService AnimalTagService { get; set; } = default!;
        private string _errorMessage = string.Empty;

        public AnimalDTO species = new();
        public List<AnimalDTO> staticParentList { get; set; } = new();
        public bool IsEditMode => ExistingCow != Guid.Empty;
        List<AnimalTypeDTO> animalTypes = new();

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

        protected override async Task OnInitializedAsync()
        {
            var types = await AnimalTypeService.GetAllAsync();
            animalTypes = types?.ToList() ?? new();

            if (animalTypes.Any())
                species.AnimalTypeCode = animalTypes.First().AnimalTypeCode;

            staticParentList = HerdList ?? new();

            if (IsEditMode)
            {
                species = HerdList?.FirstOrDefault(c => c.AnimalId == ExistingCow) ?? new AnimalDTO();
                _selectedMother = HerdList?.FirstOrDefault(c => c.AnimalId == species.MotherAnimalId);
                _selectedFather = HerdList?.FirstOrDefault(c => c.AnimalId == species.FatherAnimalId);
            }

            species.AnimalTag ??= new AnimalTagDTO
            {
                AnimalTagId = Guid.NewGuid(),
                AssignedDate = DateTime.UtcNow,
                IsCurrent = true
            };
        }

        private async Task Submit()
        {
            try
            {
                if (IsEditMode)
                {
                    await AnimalService.UpdateAsync(species);
                    await OnSubmit.InvokeAsync(species);
                }
                else
                {
                    var created = await AnimalService.CreateAsync(species);

                    if (!string.IsNullOrWhiteSpace(species.AnimalTag?.RFIDTagCode))
                    {
                        var tag = new AnimalTagDTO
                        {
                            AnimalTagId = Guid.NewGuid(),
                            AnimalId = created.AnimalId,
                            RFIDTagCode = species.AnimalTag.RFIDTagCode,
                            AssignedDate = DateTime.UtcNow,
                            IsCurrent = true
                        };
                        await AnimalTagService.CreateAsync(tag);
                        created.AnimalTag = tag;
                    }
                    else
                    {
                        created.AnimalTag = null;
                    }

                    await OnSubmit.InvokeAsync(created);
                }
            }
            catch (Exception ex)
            {
                _errorMessage = $"Failed to save animal: {ex.Message}";
                Console.WriteLine($"Error saving animal: {ex.Message}");
            }
        }

        private async Task Cancel() => await OnCancel.InvokeAsync();

        private async Task<IEnumerable<AnimalDTO>> Search(string value, CancellationToken token)
        {
            await Task.Delay(5, token);
            if (string.IsNullOrEmpty(value)) return staticParentList;
            return staticParentList.Where(x =>
                x.DisplayIdentifier.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}