
using DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HerdSync.Shared.Enums.Data;

namespace DAL.Models
{
    public class spd_Species_Detail : BaseModel
    {
        [Required]
        public required int spd_Number { get; set; }
        [Required]
        public required string spd_Tag_Colour { get; set; }
        [Required]
        public required decimal spd_Weight { get; set; }
        [Required]
        public required int spd_Age { get; set; }
        [Required]
        [MaxLength(50)]
        public required string spd_AgeGroup { get; set; } 
        [MaxLength(50)]
        public string spd_Mother { get; set; } = "Unknown";
        [MaxLength(50)]
        public string spd_Father { get; set; } = "Unknown";
        public int? spd_Est_Years_Left { get; set; }
        [MaxLength(500)]
        public string spd_Medical_Note { get; set; } = string.Empty;
        public DateTime? spd_Last_Pregnancy { get; set; }
        public int? spd_Total_Pregnancies { get; set; } = 0;
        public int? spd_Total_Offspring { get; set; } = 0;
        public bool spd_Branded { get; set; } = false;
        [Required]
        public required string spd_Species { get; set; }
        [MaxLength(50)]
        public string? spd_Gender { get; set; }
        [ForeignKey("prg_Pregnancy_Id")]
        public Guid? prg_Pregnancy_Id { get; set; } //From which pregnancy was this one born (if animal was not bought)
        public ICollection<prg_Pregnancies_Detail>? Pregnancies { get; set; }
        public bool spd_Born_Or_Buy { get; set; } = true; // 1 - Born, 0 - Buy

    }
}
