using BLL.Services;
using HerdSync.Shared.DTO.Animal;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HerdSync.Components.HerdMarking
{
    public partial class TagManagement
    {
        [Inject] public IAnimalTagService TagService { get; set; } = default!;
        [Inject] public IAnimalService AnimalService { get; set; } = default!;

        public List<AnimalTagDTO> TagList { get; set; } = new();
        public List<AnimalDTO> HerdList { get; set; } = new();
        private int TagCount;

        protected override async Task OnInitializedAsync()
        {
            TagList = (await TagService.GetAllAsync()).ToList();
            HerdList = await AnimalService.GetAllAsync();
            TagCount = TagList.Count;
        }

        private async Task OpenDialogAsync()
        {
            var parameters = new DialogParameters
            {
                ["HerdList"] = HerdList
            };
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
                if (index >= 0)
                    TagList[index] = updatedTag;
                else
                    TagList.Add(updatedTag);

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