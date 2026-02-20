using System.ComponentModel.DataAnnotations;

namespace DAL.Models.Base.History
{
    public class PregnanciesDetailHistory : BaseHistoryModel
    {
        public DateTime prg_Pregnancy_Spot_Date { get; set; }

        public DateTime prg_Pregnancy_End_Date { get; set; }

        [MaxLength(500)]
        public string prg_Notes { get; set; } = string.Empty;

        public Guid spd_Id { get; set; }
    }
}