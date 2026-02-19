using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Animal
{
    public class AnimalEventTypeDTO : BaseEntityDTO
    {
        public string EventTypeCode { get; set; }
        public string EventTypeName { get; set; }
    }
}