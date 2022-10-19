using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class Account
    {
        public Account()
        {
            Users = new HashSet<User>();
        }

        public string AccountUsername { get; set; } = null!;
        public int ToaId { get; set; }
        public string? AccountPassword { get; set; }

        public virtual Typeofaccount Toa { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; }
    }
}
