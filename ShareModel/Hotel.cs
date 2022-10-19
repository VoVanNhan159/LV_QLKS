using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Hotel
    {
        public Hotel()
        {
            Businessregistrations = new HashSet<Businessregistration>();
            ImageHotels = new HashSet<ImageHotel>();
            Rooms = new HashSet<Room>();
            Services = new HashSet<Service>();
        }

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

        public virtual User UserPhoneNavigation { get; set; } = null!;
        public virtual Ward Ward { get; set; } = null!;
        public virtual ICollection<Businessregistration> Businessregistrations { get; set; }
        public virtual ICollection<ImageHotel> ImageHotels { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
