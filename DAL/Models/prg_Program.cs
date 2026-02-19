using DAL.Models.Base;
using HerdSync.Shared.Enums.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class prg_Program : BaseModel
    {
        public required string ProgramName { get; set; }
        public DateTime? LastRunUtc { get; set; }
        public RepeatType Repeat { get; set; } = RepeatType.OnceOff;
        public ICollection<ins_Instruction_Lookup> Instructions { get; set; } = new List<ins_Instruction_Lookup>();
    }
}
