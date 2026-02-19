using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerdMark.Models
{
    public class HerdMarkRead
    {
        public string TagId { get; set; } = "";      // EPC string
        public string ReaderId { get; set; } = "";   // e.g. "finishline-1"
        public int AntennaPort { get; set; }
        public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
        public int Rssi { get; set; }
        public long Frequency { get; set; }
    }
}
