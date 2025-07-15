using HerdSync.Components.Pages;
using MudBlazor;

namespace HerdSync.Components.Pages
{

    public partial class Herd
    {
        int CowCount;

        public List<HerdDetail> HerdList = new()
        {
            new HerdDetail {Number = 10, Colour = "Green", Age = "Adult",Weight = 125},
            new HerdDetail {Number = 11, Colour = "Yellow", Age = "Adult",Weight = 132},
            new HerdDetail {Number = 12, Colour = "Green", Age = "Calf",Weight = 112},
            new HerdDetail {Number = 13, Colour = "Yellow", Age = "Adult",Weight = 121},
            new HerdDetail {Number = 14, Colour = "Green", Age = "Adult",Weight = 145},
            new HerdDetail {Number = 15, Colour = "Yellow", Age = "Calf",Weight = 136},
            new HerdDetail {Number = 16, Colour = "Green", Age = "Adult",Weight = 125},
            new HerdDetail {Number = 17, Colour = "Yellow", Age = "Adult",Weight = 154},
            new HerdDetail {Number = 18, Colour = "Green", Age = "Adult",Weight = 231}
        };

        private Task OpenDialogAsync()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };

            return DialogService.ShowAsync<NewCow>("Add New Cow", options);
        }

        private Task EditDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };

            return DialogService.ShowAsync<NewCow>("Edit Cow", options);
        }

        protected override void OnInitialized()
        {
            CowCount = HerdList.Count; 
            base.OnInitialized(); 
        }

    }
}



