using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModel.Custom
{
    public class DiscountDetail_Custom
    {
        public int DiscountId { get; set; }
        public int RoomId { get; set; }
        public int? Percent { get; set; }
    }
}
