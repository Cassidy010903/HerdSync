
using DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class prg_Pregnancies_Detail : BaseModel
    {
        [Required]
        public required DateTime prg_Pregnancy_Spot_Date { get; set; }
        [Required]
        public required DateTime prg_Pregnancy_End_Date { get; set; }
        [MaxLength(500)]
        public string prg_Notes { get; set; } = string.Empty;
        // Foreign key to spd_Species_Detail
        [ForeignKey("spd_Id")]
        [Required]
        public required Guid spd_Id { get; set; }
        public required spd_Species_Detail Species { get; set; }
    }
}
