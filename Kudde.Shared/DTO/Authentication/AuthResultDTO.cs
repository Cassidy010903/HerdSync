using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudde.Shared.DTO.Authentication
{
    public class AuthResultDTO
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public Guid? UserId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public Guid? FarmId { get; set; }
        public string FarmName { get; set; }
        public string RoleCode { get; set; }
        public bool IsSystemAdmin { get; set; }
    }
}
