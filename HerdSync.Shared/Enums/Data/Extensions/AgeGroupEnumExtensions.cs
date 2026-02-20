namespace HerdSync.Shared.Enums.Data.Extensions
{
    public static class AgeGroupEnumExtensions
    {
        public static string ToDisplayName(this AgeGroupEnum ageGroup, AnimalTypeEnum species)
        {
            return (species, ageGroup) switch
            {
                (AnimalTypeEnum.Cow, AgeGroupEnum.Calf1) => "Young Calf",
                (AnimalTypeEnum.Cow, AgeGroupEnum.Calf2) => "Older Calf",
                (AnimalTypeEnum.Cow, AgeGroupEnum.Adult) => "Adult Cow",

                (AnimalTypeEnum.Sheep, AgeGroupEnum.Calf1) => "Young Lamb",
                (AnimalTypeEnum.Sheep, AgeGroupEnum.Calf2) => "Older Lamb",
                (AnimalTypeEnum.Sheep, AgeGroupEnum.Adult) => "Adult Sheep",

                _ => "Unknown"
            };
        }
    }
}