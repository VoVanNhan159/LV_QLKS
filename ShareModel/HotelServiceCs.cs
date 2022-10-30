using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class HotelServiceCs
    {
        public int HotelId { get; set; }
        public int ServiceId { get; set; }
        public int Id { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
    }
}
