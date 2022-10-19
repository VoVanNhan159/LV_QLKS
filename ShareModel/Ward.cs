using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Ward
    {
        public Ward()
        {
            Hotels = new HashSet<Hotel>();
        }

        public string ProvinceId { get; set; } = null!;
        public string DistrictId { get; set; } = null!;
        public string WardId { get; set; } = null!;
        public string? WardName { get; set; }

        public virtual District District { get; set; } = null!;
        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
