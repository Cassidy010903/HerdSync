using BLL.Services;
using HerdSync.Shared.DTO.Animal;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HerdSync.Components.HerdMarking
{
    public partial class NewTagDialog
    {
        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }

        [Parameter]
        public Guid ExistingTag { get; set; }

        [Parameter]
        public List<AnimalTagDTO> TagList { get; set; } = new();

        [Parameter]
        public List<AnimalDTO> HerdList { get; set; } = new();

        [Inject]
        private IAnimalTagService tagService { get; set; }

        [Inject] public IAnimalService AnimalService { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        public AnimalTagDTO tag = new();
        public bool IsEditMode => ExistingTag != Guid.Empty;

        private async Task Submit()
        {
            try
            {
                if (IsEditMode)
                {
                    await tagService.UpdateAsync(tag);
                    MudDialog.Close(DialogResult.Ok(tag));
                }
                else
                {
                    await tagService.CreateAsync(tag);
                    MudDialog.Close(DialogResult.Ok(true));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving tag: {ex.Message}");
                Snackbar.Add("Failed to save tag data.", MudBlazor.Severity.Error);
            }
        }

        protected override Task OnInitializedAsync()
        {
            if (IsEditMode)
            {
                tag = TagList.FirstOrDefault(c => c.AnimalTagId == ExistingTag) ?? new AnimalTagDTO();
            }
            return Task.CompletedTask;
        }

        private void Cancel() => MudDialog.Cancel();
    }
}