
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HerdSync.Shared.Enums.Data;

namespace DAL.Models.Base.History
{
    public class SpeciesDetailHistory : BaseHistoryModel
    {
        public int spd_Number { get; set; }
        public string spd_Tag_Colour { get; set; }
        public decimal spd_Weight { get; set; }
        public int spd_Age { get; set; }
        public string spd_AgeGroup { get; set; }
        public string spd_Mother { get; set; } = "Unknown";
        public string spd_Father { get; set; } = "Unknown";
        public int? spd_Est_Years_Left { get; set; }
        public string spd_Medical_Note { get; set; } = string.Empty;
        public DateTime? spd_Last_Pregnancy { get; set; }
        public int? spd_Total_Pregnancies { get; set; } = 0;
        public int? spd_Total_Offspring { get; set; } = 0;
        public bool spd_Branded { get; set; } = false;
        public string spd_Species { get; set; }
        public string? spd_Gender { get; set; }
        public Guid? prg_Pregnancy_Id { get; set; } 
        public bool spd_Born_Or_Buy { get; set; } = true; 

    }
}
