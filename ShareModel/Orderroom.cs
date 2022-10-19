using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Orderroom
    {
        public Orderroom()
        {
            Orderroomdetails = new HashSet<Orderroomdetail>();
        }

        public int OrderroomId { get; set; }
        public string UserPhone { get; set; } = null!;
        public DateTime? OrderroomDatestart { get; set; }
        public DateTime? OrderroomDateend { get; set; }
        public string? OrderroomStatus { get; set; }
        public DateTime? OrderroomDate { get; set; }
        public int? OrderroomTotalprice { get; set; }

        public virtual User UserPhoneNavigation { get; set; } = null!;
        public virtual ICollection<Orderroomdetail> Orderroomdetails { get; set; }
    }
}
