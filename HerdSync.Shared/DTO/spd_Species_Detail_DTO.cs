using HerdSync.Shared.Enums.Data;

namespace HerdSync.Shared.DTO
{
    public class spd_Species_Detail_DTO
    {
        public Guid spd_Id { get; set; }
        public int spd_Number { get; set; }
        public ColourEnum spd_Tag_Colour { get; set; }
        public decimal spd_Weight { get; set; }
        public int spd_Age { get; set; }
        public AgeGroupEnum spd_AgeGroup { get; set; } = AgeGroupEnum.Calf1;
        public string spd_Mother { get; set; } = "Unknown";
        public string spd_Father { get; set; } = "Unknown";
        public int? spd_Est_Years_Left { get; set; }
        public string spd_Medical_Note { get; set; } = string.Empty;
        public DateTime? spd_Last_Pregnancy { get; set; }
        public int? spd_Total_Pregnancies { get; set; } = 0;
        public int? spd_Total_Offspring { get; set; } = 0;
        public bool spd_Branded { get; set; } = false;
        public AnimalTypeEnum spd_Species { get; set; }
        public GenderEnum spd_Gender { get; set; } = GenderEnum.Male;
        public Guid? prg_Pregnancy_Id { get; set; }
        public bool spd_Born_Or_Buy { get; set; } = true; // 1 - Born, 0 - Buy
    }
}