using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Discount
    {
        public Discount()
        {
            Discountdetails = new HashSet<Discountdetail>();
        }

        public int DiscountId { get; set; }
        public string UserPhone { get; set; } = null!;
        public string? DiscountName { get; set; }
        public DateTime? DiscountDate { get; set; }
        public DateTime? DiscountDatestart { get; set; }
        public DateTime? DiscountDateend { get; set; }

        public virtual User UserPhoneNavigation { get; set; } = null!;
        public virtual ICollection<Discountdetail> Discountdetails { get; set; }
    }
}
