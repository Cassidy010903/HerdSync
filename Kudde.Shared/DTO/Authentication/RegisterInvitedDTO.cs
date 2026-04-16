using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudde.Shared.DTO.Authentication
{
    public class RegisterInvitedDTO
    {
        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, MaxLength(100)]
        public string DisplayName { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }

        [Required, MaxLength(10)]
        public string InviteCode { get; set; }
    }
}
