using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Orderroomdetail
    {
        public int RoomId { get; set; }
        public int OrderroomId { get; set; }
        public int Id { get; set; }

        public virtual Orderroom Orderroom { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
}
