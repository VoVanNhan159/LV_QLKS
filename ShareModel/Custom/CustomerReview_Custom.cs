using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModel.Custom
{
    public class CustomerReview_Custom
    {
        public int RoomId { get; set; }
        public string UserPhone { get; set; } = null!;
        public DateTime? CrDate { get; set; }
        public double? CrStar { get; set; }
        public string? CrComment { get; set; }
    }
}
