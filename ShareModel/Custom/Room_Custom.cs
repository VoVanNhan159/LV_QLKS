using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModel.Custom
{
    public class Room_Custom
    {
        public int RoomId { get; set; }
        public int FloorId { get; set; }
        public int TorId { get; set; }
        public int HotelId { get; set; }
        public string? RoomName { get; set; }
        public string? RoomDescription { get; set; }
        public bool? RoomStatus { get; set; }
    }
}
