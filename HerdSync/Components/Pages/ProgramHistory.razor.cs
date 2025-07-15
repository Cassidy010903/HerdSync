namespace HerdSync.Components.Pages
{
    public partial class ProgramHistory
    {
        public List<ProgramHist> ProgramList = new()
        {
            new ProgramHist {Name = "Parasite Control", TreatGroup = "ALL", Additions = "ADDITIONAL",Date = "12 July 2025"},
            new ProgramHist {Name = "Parasite and Nutritional", TreatGroup = "ALL", Additions = "NO ADD",Date = "8 March 2025"},
            new ProgramHist {Name = "Words of Affirmation", TreatGroup = "ALL", Additions = "NO ADD",Date = "20 April 2025"}
        };
    }
}
