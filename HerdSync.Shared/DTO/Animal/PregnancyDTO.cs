using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Animal
{
    public class PregnancyDTO : BaseEntityDTO
    {
        public Guid PregnancyId { get; set; }
        public Guid MotherAnimalId { get; set; }
        public Guid? FatherAnimalId { get; set; }
        public Guid? CalfAnimalId { get; set; }
        public DateTime? ConceptionDate { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public DateTime? ExpectedBirthDate { get; set; }
        public DateTime? ActualBirthDate { get; set; }
        public string Outcome { get; set; }
        public string Notes { get; set; }
    }
}