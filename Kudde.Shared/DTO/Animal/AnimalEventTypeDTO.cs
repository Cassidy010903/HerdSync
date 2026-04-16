using Kudde.Shared.DTO.Base;

namespace Kudde.Shared.DTO.Animal
{
    public class AnimalEventTypeDTO : BaseEntityDTO
    {
        public string EventTypeCode { get; set; }
        public string EventTypeName { get; set; }
    }
}