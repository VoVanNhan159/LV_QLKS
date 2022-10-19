using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class ImageRoom
    {
        public int ImageId { get; set; }
        public int RoomId { get; set; }
        public int Id { get; set; }

        public virtual Image Image { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
}
