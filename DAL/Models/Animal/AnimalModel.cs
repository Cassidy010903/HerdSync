using DAL.Models.Animal;
using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Animal")]
public class AnimalModel : BaseEntity
{
    [Key]
    public Guid AnimalId { get; set; }

    [MaxLength(6)]
    public string? AnimalTypeCode { get; set; }

    [MaxLength(50)]
    public string? DisplayIdentifier { get; set; }

    public int? BirthYear { get; set; }

    [MaxLength(6)]
    public string Gender { get; set; } = "U";

    public decimal? Weight { get; set; }
    public Guid? MotherAnimalId { get; set; }
    public Guid? FatherAnimalId { get; set; }
    public bool IsBranded { get; set; }

    [MaxLength(500)]
    public string? MedicalNote { get; set; }

    public int? TotalPregnancies { get; set; }
    public int? TotalCalves { get; set; }
    public DateTime? LastPregnancyDate { get; set; }

    [ForeignKey("AnimalTypeCode")]
    public AnimalTypeModel? AnimalType { get; set; }

    [ForeignKey("MotherAnimalId")]
    public AnimalModel? Mother { get; set; }

    [ForeignKey("FatherAnimalId")]
    public AnimalModel? Father { get; set; }
}