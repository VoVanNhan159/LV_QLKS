using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Image
    {
        public Image()
        {
            ImageHotels = new HashSet<ImageHotel>();
            ImageRooms = new HashSet<ImageRoom>();
        }

        public int ImageId { get; set; }
        public string? ImageName { get; set; }

        public virtual ICollection<ImageHotel> ImageHotels { get; set; }
        public virtual ICollection<ImageRoom> ImageRooms { get; set; }
    }
}
