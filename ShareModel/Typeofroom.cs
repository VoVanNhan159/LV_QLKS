using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Typeofroom
    {
        public Typeofroom()
        {
            Rooms = new HashSet<Room>();
        }

        public int TorId { get; set; }
        public string UserPhone { get; set; } = null!;
        public string? TorName { get; set; }
        public int? TorPrice { get; set; }
        public int? TorCapacity { get; set; }

        public virtual User UserPhoneNavigation { get; set; } = null!;
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
