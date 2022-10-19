using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModel.Custom
{
    public class Hotel_Custom
    {
        public int HotelId { get; set; }
        public string UserPhone { get; set; } = null!;
        public string ProvinceId { get; set; } = null!;
        public string DistrictId { get; set; } = null!;
        public string WardId { get; set; } = null!;
        public string? HotelName { get; set; }
        public string? HotelX { get; set; }
        public string? HotelY { get; set; }
        public string? HotelAddress { get; set; }
        public bool? HotelStatus { get; set; }
    }
}
