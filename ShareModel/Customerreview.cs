using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Customerreview
    {
        public int RoomId { get; set; }
        public string UserPhone { get; set; } = null!;
        public int Id { get; set; }
        public DateTime? CrDate { get; set; }
        public double? CrStar { get; set; }
        public string? CrComment { get; set; }

        public virtual Room Room { get; set; } = null!;
        public virtual User UserPhoneNavigation { get; set; } = null!;
    }
}
