using HerdSync.Components.Pages;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HerdSync.Components.Pages
{
    public partial class NewCow
    {
        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }
        public string Number { get; set; } = "00";
        public string Colour { get; set; } = "Yellow";
        public int Age { get; set; }
        public string Weight { get; set; }


        private List<CowAge> cows = new()
        {
            new CowAge { ID = 1, Name = "Calf" },
            new CowAge { ID = 2, Name = "Adult" },
        };

        private void Submit() => MudDialog.Close(DialogResult.Ok(true));

        private void Cancel() => MudDialog.Cancel();
    }
}
