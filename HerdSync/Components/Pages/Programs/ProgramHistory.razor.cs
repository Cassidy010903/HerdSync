using BLL.Services;
using HerdSync.Shared.DTO.Program;
using Microsoft.AspNetCore.Components;

namespace HerdSync.Components.Pages.Programs
{
    public partial class ProgramHistory
    {
        [Inject] public IProgramRunService ProgramRunService { get; set; } = default!;

        public List<ProgramRunDTO> ProgramList { get; set; } = new();
        private bool _loading = true;

        protected override async Task OnInitializedAsync()
        {
            ProgramList = (await ProgramRunService.GetAllAsync())
                .OrderByDescending(p => p.RunDate)
                .ToList();
            _loading = false;
        }
    }
}