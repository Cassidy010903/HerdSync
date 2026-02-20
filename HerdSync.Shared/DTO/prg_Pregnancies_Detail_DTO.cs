namespace HerdSync.Shared.DTO
{
    public class prg_Pregnancies_Detail_DTO
    {
        public DateTime prg_Pregnancy_Spot_Date { get; set; }
        public DateTime prg_Pregnancy_End_Date { get; set; }
        public string prg_Notes { get; set; } = string.Empty;
        public Guid spd_Id { get; set; }
    }
}