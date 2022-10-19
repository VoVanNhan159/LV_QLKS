using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Businessregistration
    {
        public int BrId { get; set; }
        public string UserPhone { get; set; } = null!;
        public int HotelId { get; set; }
        public int PricelistbrId { get; set; }
        public bool? BrStatus { get; set; }
        public DateTime? BrDate { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;
        public virtual Pricelistbr Pricelistbr { get; set; } = null!;
        public virtual User UserPhoneNavigation { get; set; } = null!;
    }
}
