using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Floor
    {
        public Floor()
        {
            Rooms = new HashSet<Room>();
        }

        public int FloorId { get; set; }
        public string? FloorName { get; set; }
        public string? FloorDescription { get; set; }
        public int? HotelId { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
