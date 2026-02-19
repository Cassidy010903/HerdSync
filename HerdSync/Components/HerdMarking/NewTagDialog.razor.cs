using BLL.Services;
using BLL.Services.Implementation;
using FluentValidation;
using HerdSync.Shared.DTO;
using HerdSync.Shared.Enums.Data;
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
        public List<stl_Species_Tag_Lookup_DTO> TagList { get; set; } = new();
        [Parameter]
        public List<spd_Species_Detail_DTO> HerdList { get; set; } = new();

        [Inject]
        private ITagService tagService { get; set; }
        [Inject] public IAnimalService AnimalService { get; set; }
        [Inject]
        ISnackbar Snackbar { get; set; }
        public stl_Species_Tag_Lookup_DTO tag = new();
        public bool IsEditMode => ExistingTag != Guid.Empty;

        private async Task Submit()
        {

            try
            {
                if (IsEditMode)
                {
                    await tagService.UpdateTagAsync(tag);
                    MudDialog.Close(DialogResult.Ok(tag));
                }
                else
                {
                    await tagService.AddTagAsync(tag);
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
                tag = TagList.FirstOrDefault(c => c.spd_Id == ExistingTag) ?? new stl_Species_Tag_Lookup_DTO();
            }
            return Task.CompletedTask;

        }


        private void Cancel() => MudDialog.Cancel();
    }
}
