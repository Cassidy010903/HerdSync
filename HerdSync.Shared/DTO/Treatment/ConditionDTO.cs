using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Treatment
{
    public class ConditionDTO : BaseEntityDTO
    {
        public string ConditionCode { get; set; }
        public string ConditionName { get; set; }
        public string ConditionDescription { get; set; }
        public bool IsInfectious { get; set; }
    }
}