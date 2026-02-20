using DAL.Models.Base;
using HerdSync.Shared.Enums.Data.Extensions;

namespace DAL.Models
{
    public class prg_Program : BaseModel
    {
        public string ProgramName { get; set; }
        public DateTime? LastRunUtc { get; set; }
        public RepeatType Repeat { get; set; } = RepeatType.OnceOff;
        public ICollection<ins_Instruction_Lookup> Instructions { get; set; } = new List<ins_Instruction_Lookup>();
    }
}