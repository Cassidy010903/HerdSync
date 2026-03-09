using BLL.Services;
using HerdSync.Shared.DTO.Program;
using Microsoft.AspNetCore.Components;

namespace HerdSync.Components.Pages.Programs
{
    public partial class ProgramHistory
    {
        [Inject] public IProgramRunService ProgramRunService { get; set; } = default!;
        [Inject] public IProgramTemplateService ProgramTemplateService { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        public List<IGrouping<DateTime, ProgramRunDTO>> GroupedRuns { get; set; } = new();
        private Dictionary<string, string> _templateNames = new();
        private bool _loading = true;

        protected override async Task OnInitializedAsync()
        {
            var runs = (await ProgramRunService.GetAllAsync())
                .OrderByDescending(p => p.RunDate)
                .ToList();

            var templates = await ProgramTemplateService.GetAllAsync();
            _templateNames = templates.ToDictionary(t => t.ProgramTemplateCode, t => t.TemplateName);

            GroupedRuns = runs
                .GroupBy(p => p.RunDate.Date)
                .ToList();

            _loading = false;
        }

        private string GetTemplateName(string code)
            => _templateNames.TryGetValue(code, out var name) ? name : code;

        private void ViewRun(Guid programRunId)
            => Nav.NavigateTo($"/programhistory/{programRunId}");
    }
}