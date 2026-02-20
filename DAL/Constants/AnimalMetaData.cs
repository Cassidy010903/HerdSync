using DAL.MetaData;
using HerdSync.Shared.Enums.Data;

namespace DAL.Constants
{
    public static class AnimalMetadata
    {
        public static readonly Dictionary<AnimalTypeEnum, AnimalInfo> AnimalMap = new()
        {
            {
                AnimalTypeEnum.Cow,
                new AnimalInfo
                {
                    GenderOptions = new List<GenderEnum> { GenderEnum.Female, GenderEnum.Male },
                    AgeGroup = new List<AgeGroupEnum> { AgeGroupEnum.Calf1, AgeGroupEnum.Calf2, AgeGroupEnum.Adult }
                }
            },
            {
                AnimalTypeEnum.Sheep,
                new AnimalInfo
                {
                    GenderOptions = new List<GenderEnum> { GenderEnum.Female, GenderEnum.Male },
                    AgeGroup = new List<AgeGroupEnum> { AgeGroupEnum.Calf1, AgeGroupEnum.Adult }
                }
            }
        };
    }
}