using BLL.Services;
using HerdSync.Shared.DTO.Program;
using Microsoft.AspNetCore.Components;

namespace HerdSync.Components.Pages.Programs
{
    public partial class Programs
    {
        [Inject] private IProgramTemplateService ProgramTemplateService { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;

        private bool _loading = true;
        private List<ProgramTemplateDTO> _programs = new();

        protected override async Task OnInitializedAsync()
        {
            var result = await ProgramTemplateService.GetAllAsync();
            _programs = result.OrderByDescending(p => p.CreatedDate).ToList();
            _loading = false;
        }
    }
}