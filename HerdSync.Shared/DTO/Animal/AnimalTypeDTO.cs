using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Animal
{
    public class AnimalTypeDTO : BaseEntityDTO
    {
        public string AnimalTypeCode { get; set; }
        public string AnimalTypeName { get; set; }
        public string PlaceholderImage { get; set; }
    }
}