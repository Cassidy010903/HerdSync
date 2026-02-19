using DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class stl_Species_Tag_Lookup : BaseModel
    {
        public required string stl_Tag_Id { get; set; }
        public Guid spd_Id { get; set; }
        [ForeignKey("spd_Id")]
        public spd_Species_Detail Species { get; set; }
    }
}
