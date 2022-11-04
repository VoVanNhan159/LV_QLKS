using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModel.Custom
{
    public class Account_Custom
    {
        public string AccountUsername { get; set; } = null!;
        public int ToaId { get; set; }
        public string? AccountPassword { get; set; }
    }
}
