using BLL.Services;
using BLL.Services.Implementation;
using HerdSync.Components.Pages;
using HerdSync.Shared;
using HerdSync.Shared.DTO;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HerdSync.Components.Pages
{

    public partial class Herd
    {
        [Inject] public IAnimalService AnimalService { get; set; }
        public List<spd_Species_Detail_DTO> HerdList { get; set; } = new();
        int CowCount;
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

            if (result.Data is spd_Species_Detail_DTO updatedCow)
            {
                var index = HerdList.FindIndex(c => c.spd_Id == updatedCow.spd_Id);
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



