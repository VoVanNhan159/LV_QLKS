using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModel.Custom
{
    public class Orderroom_Custom
    {
        public int OrderroomId { get; set; }
        public string UserPhone { get; set; } = null!;
        public DateTime? OrderroomDatestart { get; set; }
        public DateTime? OrderroomDateend { get; set; }
        public string? OrderroomStatus { get; set; }
        public DateTime? OrderroomDate { get; set; }
        public int? OrderroomTotalprice { get; set; }
    }
}
