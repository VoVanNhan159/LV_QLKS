using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Service
    {
        public int ServiceId { get; set; }
        public string UserPhone { get; set; } = null!;
        public int? HotelId { get; set; }
        public string? ServiceName { get; set; }
        public string? ServiceDescription { get; set; }

        public virtual Hotel? Hotel { get; set; }
        public virtual User UserPhoneNavigation { get; set; } = null!;
    }
}
