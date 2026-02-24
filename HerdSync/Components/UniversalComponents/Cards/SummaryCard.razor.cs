using Microsoft.AspNetCore.Components;

namespace HerdSync.Components.UniversalComponents.Cards
{
    public partial class SummaryCard
    {
        [Parameter] public string ColClass { get; set; } = "col-xl-3 col-md-6 mb-4";
        [Parameter] public string BorderHex { get; set; } = "#4e73df";
        [Parameter] public string Icon { get; set; } = "fas fa-info-circle";
        [Parameter] public string Title { get; set; } = string.Empty;
        [Parameter] public string Value { get; set; } = string.Empty;
    }
}