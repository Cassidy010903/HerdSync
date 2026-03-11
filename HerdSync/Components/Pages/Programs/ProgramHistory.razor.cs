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

        private List<ProgramRunDTO> _allRuns = new();
        private Dictionary<string, string> _templateNames = new();
        private bool _loading = true;

        private string _searchTerm = string.Empty;
        private string _programFilter = string.Empty;
        private DateTime? _dateFrom = null;
        private DateTime? _dateTo = null;
        private bool _filterPanelOpen = false;

        private List<IGrouping<DateTime, ProgramRunDTO>> GroupedRuns =>
            _allRuns
                .Where(r =>
                    (string.IsNullOrWhiteSpace(_searchTerm) ||
                     GetTemplateName(r.ProgramTemplateCode).Contains(_searchTerm, StringComparison.OrdinalIgnoreCase) ||
                     r.ProgramTemplateCode.Contains(_searchTerm, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(_programFilter) || r.ProgramTemplateCode == _programFilter) &&
                    (_dateFrom == null || r.RunDate.Date >= _dateFrom.Value.Date) &&
                    (_dateTo == null || r.RunDate.Date <= _dateTo.Value.Date)
                )
                .OrderByDescending(r => r.RunDate)
                .GroupBy(r => r.RunDate.Date)
                .ToList();

        private List<KeyValuePair<string, string>> _availablePrograms => _templateNames
            .Select(t => new KeyValuePair<string, string>(t.Key, t.Value))
            .OrderBy(t => t.Value)
            .ToList();

        protected override async Task OnInitializedAsync()
        {
            _allRuns = (await ProgramRunService.GetAllAsync())
                .OrderByDescending(p => p.RunDate)
                .ToList();

            var templates = await ProgramTemplateService.GetAllAsync();
            _templateNames = templates.ToDictionary(t => t.ProgramTemplateCode, t => t.TemplateName);

            _loading = false;
        }

        private string GetTemplateName(string code)
            => _templateNames.TryGetValue(code, out var name) ? name : code;

        private void ViewRun(Guid programRunId)
            => Nav.NavigateTo($"/programhistory/{programRunId}");

        private void OnSearchChanged(string value) { _searchTerm = value; StateHasChanged(); }
        private void ToggleFilterPanel() => _filterPanelOpen = !_filterPanelOpen;

        private void ClearFilters()
        {
            _searchTerm = string.Empty;
            _programFilter = string.Empty;
            _dateFrom = null;
            _dateTo = null;
            _filterPanelOpen = false;
            StateHasChanged();
        }

        private bool HasActiveFilters =>
            !string.IsNullOrEmpty(_programFilter) || _dateFrom != null || _dateTo != null;
    }
}