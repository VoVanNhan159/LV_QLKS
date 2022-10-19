using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Typeofaccount
    {
        public Typeofaccount()
        {
            Accounts = new HashSet<Account>();
        }

        public int ToaId { get; set; }
        public string? ToaPower { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
