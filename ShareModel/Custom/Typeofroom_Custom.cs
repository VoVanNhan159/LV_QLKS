using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModel.Custom
{
    public class Typeofroom_Custom
    {
        public int TorId { get; set; }
        public string UserPhone { get; set; } = null!;
        public string? TorName { get; set; }
        public int? TorPrice { get; set; }
        public int? TorCapacity { get; set; }
    }
}
