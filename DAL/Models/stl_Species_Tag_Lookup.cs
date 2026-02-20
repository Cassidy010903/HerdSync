using DAL.Models.Animal;
using DAL.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class stl_Species_Tag_Lookup : BaseModel
    {
        public string stl_Tag_Id { get; set; }
        public Guid spd_Id { get; set; }

        [ForeignKey("spd_Id")]
        public AnimalModel Species { get; set; }
    }
}