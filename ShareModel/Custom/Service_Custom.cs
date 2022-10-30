using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModel.Custom
{
    public class Service_Custom
    {
        public int ServiceId { get; set; }
        public string UserPhone { get; set; } = null!;
        public string? ServiceName { get; set; }
        public string? ServiceDescription { get; set; }
    }
}
