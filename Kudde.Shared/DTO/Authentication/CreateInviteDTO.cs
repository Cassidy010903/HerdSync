using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudde.Shared.DTO.Authentication
{
    public class CreateInviteDTO
    {
        public Guid FarmId { get; set; }

        [Required, MaxLength(4)]
        public string AssignedRoleCode { get; set; }

        public int ExpiryHours { get; set; } = 48;
    }
}
