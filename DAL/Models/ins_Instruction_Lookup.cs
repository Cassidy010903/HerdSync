using DAL.Models.Base;
using HerdSync.Shared.Enums.Data;
using System.ComponentModel.DataAnnotations.Schema;

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