using HerdSync.Shared.Enums.Data;

namespace DAL.MetaData
{
    public class AnimalInfo
    {
        public List<GenderEnum>? GenderOptions { get; set; }
        public List<AgeGroupEnum>? AgeGroup { get; set; }

        //Usage tip:
        /*
        var displayName = AgeGroupEnum.Calf1.ToDisplayName();
        Console.WriteLine(displayName); // Output: "Young Calf"
         */
    }
}