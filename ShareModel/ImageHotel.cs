using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class ImageHotel
    {
        public int ImageId { get; set; }
        public int HotelId { get; set; }
        public int Id { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;
        public virtual Image Image { get; set; } = null!;
    }
}
