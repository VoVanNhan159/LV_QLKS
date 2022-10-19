using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class District
    {
        public District()
        {
            Wards = new HashSet<Ward>();
        }

        public string ProvinceId { get; set; } = null!;
        public string DistrictId { get; set; } = null!;
        public string? DistrictName { get; set; }

        public virtual Province Province { get; set; } = null!;
        public virtual ICollection<Ward> Wards { get; set; }
    }
}
