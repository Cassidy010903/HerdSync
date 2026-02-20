using DAL.Models.Base;

namespace DAL.Models
{
    public class trl_Treatment_Lookup : BaseModel
    {
        public string TreatmentName { get; set; }
        public string? Description { get; set; }
        public string? DefaultDose { get; set; }
    }
}