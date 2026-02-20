using BLL.Services;
using HerdSync.Shared.DTO.Animal;
using Microsoft.AspNetCore.Components;

namespace HerdSync.Components.Pages.HerdManagement
{
    public partial class Herd
    {
        [Inject] public IAnimalService AnimalService { get; set; }
        public List<AnimalDTO> HerdList { get; set; } = new();
        private int CowCount;

        private async Task OpenDialogAsync()
        {
            //var options = new DialogOptions { CloseOnEscapeKey = true };

            //return DialogService.ShowAsync<NewCow>("Add New Cow", options);
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var dialog = await DialogService.ShowAsync<NewCow>("Add New Cow", options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                HerdList = await AnimalService.GetAllHerdAsync();
                CowCount = HerdList.Count;
                StateHasChanged();
            }
        }

        private async Task EditDialog(Guid cow)
        {
            var parameters = new DialogParameters
            {
                ["ExistingCow"] = cow,
                ["HerdList"] = HerdList
            };

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var dialog = await DialogService.ShowAsync<NewCow>("Edit Cow", parameters, options);
            var result = await dialog.Result;

            if (result.Data is AnimalDTO updatedCow)
            {
                var index = HerdList.FindIndex(c => c.AnimalId == updatedCow.AnimalId);
                if (index >= 0)
                    HerdList[index] = updatedCow;
                else
                    HerdList.Add(updatedCow);

                CowCount = HerdList.Count;
                StateHasChanged();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            HerdList = await AnimalService.GetAllHerdAsync();
            CowCount = HerdList.Count;
            base.OnInitialized();
        }
    }
}