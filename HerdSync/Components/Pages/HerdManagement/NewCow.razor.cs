using BLL.Services;
using FluentValidation;
using HerdSync.Shared.DTO.Animal;
using HerdSync.Shared.Enums.Data;
using HerdSync.Shared.Enums.Data.Extensions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HerdSync.Components.Pages.HerdManagement
{
    public partial class NewCow
    {
        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }

        [Parameter]
        public Guid ExistingCow { get; set; }

        [Parameter]
        public List<AnimalDTO> HerdList { get; set; } = new();

        [Inject]
        private IAnimalService animalService { get; set; }

        [Inject]
        private IValidator<AnimalDTO> SpeciesValidator { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        public AnimalDTO species = new();
        public string Branded { get; set; }
        public bool IsEditMode => ExistingCow != Guid.Empty;

        public List<(AgeGroupEnum Value, string Display)> AgeGroupDisplayOptions =>
            Enum.GetValues<AgeGroupEnum>()
                .Cast<AgeGroupEnum>()
                .Select(a => (a, a.ToDisplayName(AnimalTypeEnum.Cow)))
                .ToList();

        public List<(GenderEnum Value, string Display)> GenderDisplayOptions =>
            Enum.GetValues<GenderEnum>()
                .Cast<GenderEnum>()
                .Select(g => (g, g.ToDisplayGender(AnimalTypeEnum.Cow))) //TODO: Update this according to actual AnimalType
                .ToList();

        public List<(ColourEnum Value, string Display)> ColourDisplayOptions =>
            Enum.GetValues<ColourEnum>()
                .Cast<ColourEnum>()
                .Select(g => (g, g.ToDisplayColour()))
                .ToList();

        private string GetBranded()
        {
            if (species.IsBranded == true)
            {
                Branded = "Branded";
            }
            else
            {
                Branded = "Unbranded";
            }

            return Branded;
        }

        private async Task Submit()
        {
            //var result = await SpeciesValidator.ValidateAsync(species);
            //if (!result.IsValid)
            //{
            //    foreach (var error in result.Errors)
            //    {
            //        Snackbar.Add($"{error.PropertyName}: {error.ErrorMessage}", MudBlazor.Severity.Error);
            //    }
            //    return;
            //}

            try
            {
                if (IsEditMode)
                {
                    await animalService.UpdateAnimalAsync(species);
                    MudDialog.Close(DialogResult.Ok(species));
                }
                else
                {
                    await animalService.AddAnimalAsync(species);
                    MudDialog.Close(DialogResult.Ok(true));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving animal: {ex.Message}");
                Snackbar.Add("Failed to save cow data.", MudBlazor.Severity.Error);
            }
        }

        protected override Task OnInitializedAsync()
        {
            if (IsEditMode)
            {
                species = HerdList.FirstOrDefault(c => c.AnimalId == ExistingCow) ?? new AnimalDTO();
            }
            return Task.CompletedTask;
        }

        private void Cancel() => MudDialog.Cancel();
    }
}