using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class LiveSessionService
    {
        public bool IsActive { get; private set; }
        public event Action? OnChanged;

        public void StartSession()
        {
            IsActive = true;
            OnChanged?.Invoke();
        }

        public void EndSession()
        {
            IsActive = false;
            OnChanged?.Invoke();
        }
    }
}
