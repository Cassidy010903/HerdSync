using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerdSync.Shared.Enums.Data.Extensions
{
    public static class GenderExtensionEnum
    {
        public static string ToDisplayGender(this GenderEnum gender, AnimalTypeEnum species)
        {
            return (species, gender) switch
            {
                (AnimalTypeEnum.Cow, GenderEnum.Male) => "Bull/Ox",
                (AnimalTypeEnum.Cow, GenderEnum.Female) => "Cow/Heifer",

                (AnimalTypeEnum.Sheep, GenderEnum.Male) => "Ram/Wether",
                (AnimalTypeEnum.Sheep, GenderEnum.Female) => "Ewe",

                _ => "Unknown"
            };
        }
    }
}
