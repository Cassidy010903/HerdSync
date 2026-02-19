using DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HerdSync.Shared.Enums.Data;

namespace DAL.Models
{
    public class ins_Instruction_Lookup : BaseModel
    {
        public Guid ProgramId { get; set; }
        [ForeignKey("ProgramId")]
        public prg_Program Program { get; set; } = default!;

        public AgeGroupEnum? TargetGroup { get; set; }
        public GenderEnum? TargetGender { get; set; }
        public AnimalTypeEnum? TargetSpecies { get; set; }

        public ICollection<itr_Instruction_Treatment> Treatments { get; set; } = new List<itr_Instruction_Treatment>();
    }
}
