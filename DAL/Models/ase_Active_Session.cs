using DAL.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class ase_Active_Session : BaseModel
    {
        public Guid ProgramId { get; set; }

        [ForeignKey("ProgramId")]
        public prg_Program Program { get; set; } = default!;

        public DateTime StartedUtc { get; set; } = DateTime.UtcNow;
        public DateTime? EndedUtc { get; set; }
        public string? Notes { get; set; }
        public bool IsClosed { get; set; } = false;
        public ICollection<ast_Animal_Session_Treatment> Animals { get; set; } = new List<ast_Animal_Session_Treatment>();
    }
}