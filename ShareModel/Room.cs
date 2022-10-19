using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Room
    {
        public Room()
        {
            Customerreviews = new HashSet<Customerreview>();
            Discountdetails = new HashSet<Discountdetail>();
            ImageRooms = new HashSet<ImageRoom>();
            Orderroomdetails = new HashSet<Orderroomdetail>();
        }

        public int RoomId { get; set; }
        public int FloorId { get; set; }
        public int TorId { get; set; }
        public int HotelId { get; set; }
        public string? RoomName { get; set; }
        public string? RoomDescription { get; set; }
        public bool? RoomStatus { get; set; }

        public virtual Floor Floor { get; set; } = null!;
        public virtual Hotel Hotel { get; set; } = null!;
        public virtual Typeofroom Tor { get; set; } = null!;
        public virtual ICollection<Customerreview> Customerreviews { get; set; }
        public virtual ICollection<Discountdetail> Discountdetails { get; set; }
        public virtual ICollection<ImageRoom> ImageRooms { get; set; }
        public virtual ICollection<Orderroomdetail> Orderroomdetails { get; set; }
    }
}
