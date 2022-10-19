using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModel.Custom
{
    public class Discount_Custom
    {
        public int DiscountId { get; set; }
        public string UserPhone { get; set; } = null!;
        public string? DiscountName { get; set; }
        public DateTime? DiscountDate { get; set; }
        public DateTime? DiscountDatestart { get; set; }
        public DateTime? DiscountDateend { get; set; }
    }
}
