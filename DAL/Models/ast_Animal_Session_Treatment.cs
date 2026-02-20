using DAL.Models.Base;
using HerdSync.Shared.Enums.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class ast_Animal_Session_Treatment : BaseModel
    {
        public Guid SessionId { get; set; }

        [ForeignKey("SessionId")]
        public ase_Active_Session Session { get; set; } = default!;

        public Guid spd_Id { get; set; }                 // FK to your animal
        public string TagId { get; set; } = "";          // what was scanned
        public AgeGroupEnum AgeGroup { get; set; }           // resolved at scan-time
        public DateTime ScannedUtc { get; set; }         // from read

        public bool AutoApplied { get; set; } = true;    // program applied treatments
        public string? OperatorNotes { get; set; }

        public ICollection<atr_Animal_Treatment> Treatments { get; set; } = new List<atr_Animal_Treatment>();
    }
}