using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Animal
{
    public class AnimalDTO : BaseEntityDTO
    {
        public Guid AnimalId { get; set; }
        public string? AnimalTypeCode { get; set; }
        public string? DisplayIdentifier { get; set; }
        public int? BirthYear { get; set; }
        public string? Gender { get; set; }
        public decimal? Weight { get; set; }
        public Guid? MotherAnimalId { get; set; }
        public Guid? FatherAnimalId { get; set; }
        public bool IsBranded { get; set; }
        public string? MedicalNote { get; set; }
        public int? TotalPregnancies { get; set; }
        public int? TotalCalves { get; set; }
        public DateTime? LastPregnancyDate { get; set; }
        public AnimalTagDTO? AnimalTag { get; set; }
    }
}