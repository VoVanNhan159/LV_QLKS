using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModel.Custom
{
    public class Businessregistration_Custom
    {
        public int BrId { get; set; }
        public string UserPhone { get; set; } = null!;
        public int HotelId { get; set; }
        public int PricelistbrId { get; set; }
        public bool? BrStatus { get; set; }
        public DateTime? BrDate { get; set; }
    }
}
