using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerdSync.Shared.DTO
{
    public class stl_Species_Tag_Lookup_DTO
    {
        public Guid stl_Id { get; set; }
        public string stl_Tag_Id { get; set; }
        public Guid spd_Id { get; set; }
    }
}
