using BLL.Services;
using Kudde.Shared.DTO.Animal;
using Microsoft.AspNetCore.Components;

namespace Kudde.Components.Pages.Herd
{
    public partial class NewTagDialog
    {
        [Parameter] public Guid ExistingTag { get; set; }
        [Parameter] public List<AnimalTagDTO> TagList { get; set; } = new();
        [Parameter] public List<AnimalDTO> HerdList { get; set; } = new();
        [Parameter] public EventCallback<AnimalTagDTO> OnSubmit { get; set; }
        [Parameter] public EventCallback OnCancel { get; set; }
        [Inject] private IAnimalTagService tagService { get; set; } = default!;

        public AnimalTagDTO tag = new();
        public bool IsEditMode => ExistingTag != Guid.Empty;

        protected override Task OnInitializedAsync()
        {
            if (IsEditMode)
                tag = TagList.FirstOrDefault(c => c.AnimalTagId == ExistingTag) ?? new AnimalTagDTO();
            return Task.CompletedTask;
        }

        private async Task Submit()
        {
            try
            {
                tag.AssignedDate = DateTime.Now;
                if (IsEditMode)
                    await tagService.UpdateAsync(tag);
                else
                    await tagService.CreateAsync(tag);

                await OnSubmit.InvokeAsync(tag);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving tag: {ex.Message}");
            }
        }

        private async Task Cancel() => await OnCancel.InvokeAsync();
    }
}