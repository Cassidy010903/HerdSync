using BLL.Services;
using BLL.Services.Implementation;
using HerdSync.Components.Pages.HerdManagement;
using HerdSync.Shared.DTO;
using HerdSync.Shared.DTO.Animal;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HerdSync.Components.HerdMarking
{
    public partial class TagManagement
    {
        [Inject] public ITagService TagService { get; set; }
        [Inject] public IAnimalService AnimalService { get; set; }
        public List<stl_Species_Tag_Lookup_DTO> TagList { get; set; } = new();
        public List<AnimalDTO> HerdList { get; set; } = new();
        int TagCount;
        private async Task OpenDialogAsync()
        {
            var parameters = new DialogParameters
            {
               ["HerdList"] = HerdList
            };

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var dialog = await DialogService.ShowAsync<NewTagDialog>("Add New Tag",parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                TagList = await TagService.GetAllTagsAsync();
                TagCount = TagList.Count;
                StateHasChanged();
            }

        }

        private async Task EditDialog(string tagID)
        {
            var parameters = new DialogParameters
            {
                ["ExistingTag"] = tagID,
                ["TagList"] = TagList,
                ["HerdList"] = HerdList
            };

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var dialog = await DialogService.ShowAsync<NewTagDialog>("Edit Tag", parameters, options);
            var result = await dialog.Result;

            if (result.Data is stl_Species_Tag_Lookup_DTO updatedTag)
            {
                var index = TagList.FindIndex(c => c.spd_Id == updatedTag.spd_Id);
                if (index >= 0)
                    TagList[index] = updatedTag;
                else
                    TagList.Add(updatedTag);

                TagCount = TagList.Count;
                StateHasChanged();
            }

        }

        protected override async Task OnInitializedAsync()
        {
            TagList = await TagService.GetAllTagsAsync();
            //HerdList = await AnimalService.GetAllHerdAsync();
            TagCount = TagList.Count;
            base.OnInitialized();
        }

        private string GetAnimalInfo(Guid id)
        {
            AnimalDTO match = HerdList.FirstOrDefault(s => s.AnimalId == id);
            string animalNumber = match.DisplayIdentifier.ToString();
            return animalNumber ?? string.Empty;
        }
    }
}
