using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudde.Shared.DTO.Authentication
{
    public class InviteResultDTO
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string InviteCode { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
