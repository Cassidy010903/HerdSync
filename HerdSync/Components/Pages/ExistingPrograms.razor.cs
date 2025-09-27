namespace HerdSync.Components.Pages
{
    public partial class ExistingPrograms
    {
        private List<ProgramHerd> programs = new()
        {
            new ProgramHerd { ProgramName = "Annual Rabies" , Description = "Annual Rabies shots for cows", TreatGroup = "All Cows", Repeat = "Annual"},
            new ProgramHerd { ProgramName = "Parasite Control" , Description = "Flea dip treatments", TreatGroup = "All Cows", Repeat = "Bi-Annual"},
            new ProgramHerd { ProgramName = "Tag Check" , Description = "Check functionality of tags", TreatGroup = "Permanent Cows", Repeat = "Quarterly"},
            new ProgramHerd { ProgramName = "Hoof Care" , Description = "Hoof health check", TreatGroup = "All Cows", Repeat = "Bi-Annual"},
            new ProgramHerd { ProgramName = "Reproductive Management" , Description = "Looking after pregnant queens", TreatGroup = "Pregnant Cows", Repeat = "Bi-Annual"},
            new ProgramHerd { ProgramName = "Nutritional Management" , Description = "Checking that the babies are healthy", TreatGroup = "All calfs", Repeat = "Quarterly"}

        };
    }
}
