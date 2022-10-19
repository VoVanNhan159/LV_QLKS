using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Pricelistbr
    {
        public Pricelistbr()
        {
            Businessregistrations = new HashSet<Businessregistration>();
        }

        public int PricelistbrId { get; set; }
        public string? PricelistbrName { get; set; }
        public int? PricelistbrPrice { get; set; }
        public int? PricelistbrMonth { get; set; }

        public virtual ICollection<Businessregistration> Businessregistrations { get; set; }
    }
}
