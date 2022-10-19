using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModel.Custom
{
    public class Orderroom_Orderroomdetail_Room
    {
        public Orderroom_Orderroomdetail_Room()
        {
            Orderroomdetails = new HashSet<Orderroomdetail>();
            Rooms = new HashSet<Room>();
        }
        public int OrderroomId { get; set; }
        public DateTime? OrderroomDatestart { get; set; }
        public DateTime? OrderroomDateend { get; set; }
        public string? OrderroomStatus { get; set; }
        public DateTime? OrderroomDate { get; set; }
        public int? OrderroomTotalprice { get; set; }
        public virtual ICollection<Orderroomdetail> Orderroomdetails { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
