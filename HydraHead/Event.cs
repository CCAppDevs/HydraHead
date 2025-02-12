using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydraHead
{
    public class Event
    {
        public string Description { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public DateTime Timestamp { get; set; }
        public string HostAddress { get; set; }
    }
}
