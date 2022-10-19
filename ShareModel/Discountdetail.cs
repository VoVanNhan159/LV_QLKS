using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Discountdetail
    {
        public int DiscountId { get; set; }
        public int RoomId { get; set; }
        public int? Percent { get; set; }

        public virtual Discount Discount { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
}
