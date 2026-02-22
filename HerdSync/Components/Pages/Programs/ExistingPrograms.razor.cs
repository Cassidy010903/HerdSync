using BLL.Services;
using HerdSync.Shared.DTO.Program;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HerdSync.Components.Pages.Programs
{
    public partial class ExistingPrograms
    {
        [Inject] private IProgramRunService ProgramRunService { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;

        private List<ProgramRunDTO> programs = new();
        private bool loading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadPrograms();
        }

        private async Task LoadPrograms()
        {
            loading = true;
            programs = (await ProgramRunService.GetAllAsync()).ToList();
            loading = false;
        }

        private async Task StartProgram(Guid programRunId)
        {
            var confirm = await DialogService.ShowMessageBox(
                "Start Program",
                "This will start an active session. You must complete all treatments before exiting.",
                yesText: "Start",
                cancelText: "Cancel"
            );

            if (confirm == true)
            {
                Nav.NavigateTo("/livelist");
            }
        }
    }
}