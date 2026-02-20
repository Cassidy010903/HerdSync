using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Components;

namespace HerdSync.Components.Pages.Programs
{
    public partial class ExistingPrograms
    {
        [Inject] private IProgramService ProgramService { get; set; } = default!;
        [Inject] private ISessionService SessionService { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;

        private List<prg_Program> programs = new();
        private bool loading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadPrograms();
        }

        private async Task LoadPrograms()
        {
            loading = true;
            programs = await ProgramService.ListAsync();
            loading = false;
        }

        private async Task StartProgram(Guid programId)
        {
            var confirm = await DialogService.ShowMessageBox(
                "Start Program",
                "This will start an active session. You must complete all treatments before exiting.",
                yesText: "Start",
                cancelText: "Cancel"
            );

            if (confirm == true)
            {
                await SessionService.StartAsync(programId);
                Nav.NavigateTo("/livelist");
            }
        }
    }
}