using DAL.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class itr_Instruction_Treatment : BaseModel
    {
        public Guid InstructionId { get; set; }

        [ForeignKey("InstructionId")]
        public ins_Instruction_Lookup Instruction { get; set; } = default!;

        public Guid TreatmentId { get; set; }

        [ForeignKey("TreatmentId")]
        public trl_Treatment_Lookup Treatment { get; set; } = default!;

        public int SortOrder { get; set; } = 0;
    }
}