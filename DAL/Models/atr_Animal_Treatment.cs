using DAL.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class atr_Animal_Treatment : BaseModel
    {
        public Guid AnimalActionId { get; set; }

        [ForeignKey("AnimalActionId")]
        public ast_Animal_Session_Treatment AnimalAction { get; set; } = default!;

        public Guid TreatmentId { get; set; }

        [ForeignKey("TreatmentId")]
        public trl_Treatment_Lookup Treatment { get; set; } = default!;

        public bool IsDefault { get; set; } = true;      // from program vs added ad-hoc
        public string? DoseOverride { get; set; }        // optional
        public DateTime AppliedUtc { get; set; } = DateTime.UtcNow;
    }
}