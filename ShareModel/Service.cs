using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Service
    {
        public Service()
        {
            HotelServices = new HashSet<HotelServiceCs>();
        }

        public int ServiceId { get; set; }
        public string UserPhone { get; set; } = null!;
        public string? ServiceName { get; set; }
        public string? ServiceDescription { get; set; }

        public virtual User UserPhoneNavigation { get; set; } = null!;
        public virtual ICollection<HotelServiceCs> HotelServices { get; set; }
    }
}
