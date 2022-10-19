using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Province
    {
        public Province()
        {
            Districts = new HashSet<District>();
        }

        public string ProvinceId { get; set; } = null!;
        public string? ProvinceName { get; set; }

        public virtual ICollection<District> Districts { get; set; }
    }
}
