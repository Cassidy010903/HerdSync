using BLL.Services;
using HerdSync.Shared.DTO.Animal;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HerdSync.Components.Pages.HerdManagement
{
    public partial class Herd
    {
        [Inject] public IAnimalService AnimalService { get; set; } = default!;
        private bool _loading = true;
        public List<AnimalDTO> HerdList { get; set; } = new();

        private int CowCount;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _loading = true;
                StateHasChanged();

                HerdList = await AnimalService.GetAllAsync();
                CowCount = HerdList.Count;
                _loading = false;
                StateHasChanged();
            }
        }

        protected override Task OnInitializedAsync()
        {
            return Task.CompletedTask;
        }

        private async Task OpenDialogAsync()
        {
            var parameters = new DialogParameters
            {
                ["HerdList"] = HerdList
            };
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var dialog = await DialogService.ShowAsync<NewCow>("Add New Cow", parameters, options);
            var result = await dialog.Result;
            if (!result.Canceled)
            {
                HerdList = await AnimalService.GetAllAsync();
                CowCount = HerdList.Count;
                StateHasChanged();
            }
        }

        private async Task EditDialog(Guid animalId)
        {
            var parameters = new DialogParameters
            {
                ["ExistingCow"] = animalId,
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
    }
}