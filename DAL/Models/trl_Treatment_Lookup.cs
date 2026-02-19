using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models.Base;

namespace DAL.Models
{
    public class trl_Treatment_Lookup : BaseModel
    {
        public required string TreatmentName { get; set; }   
        public string? Description { get; set; }
        public string? DefaultDose { get; set; }
    }
}
