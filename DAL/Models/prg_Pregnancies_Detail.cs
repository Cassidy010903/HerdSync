using DAL.Models.Animal;
using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class prg_Pregnancies_Detail : BaseModel
    {
        [Required]
        public DateTime prg_Pregnancy_Spot_Date { get; set; }

        [Required]
        public DateTime prg_Pregnancy_End_Date { get; set; }

        [MaxLength(500)]
        public string prg_Notes { get; set; } = string.Empty;

        // Foreign key to spd_Species_Detail
        [ForeignKey("spd_Id")]
        [Required]
        public Guid spd_Id { get; set; }

        public AnimalModel Species { get; set; }
    }
}