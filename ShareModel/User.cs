using System;
using System.Collections.Generic;

namespace ShareModel
{
    public partial class User
    {
        public User()
        {
            Businessregistrations = new HashSet<Businessregistration>();
            Customerreviews = new HashSet<Customerreview>();
            Discounts = new HashSet<Discount>();
            Hotels = new HashSet<Hotel>();
            Orderrooms = new HashSet<Orderroom>();
            Services = new HashSet<Service>();
            Typeofrooms = new HashSet<Typeofroom>();
        }

        public string UserPhone { get; set; } = null!;
        public string? AccountUsername { get; set; }
        public string? UserName { get; set; }
        public DateTime? UserDateofbirth { get; set; }
        public string? UserId { get; set; }
        public string? UserAddress { get; set; }
        public string? UserEmail { get; set; }
        public bool? UserSex { get; set; }

        public virtual Account? AccountUsernameNavigation { get; set; }
        public virtual ICollection<Businessregistration> Businessregistrations { get; set; }
        public virtual ICollection<Customerreview> Customerreviews { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
        public virtual ICollection<Orderroom> Orderrooms { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<Typeofroom> Typeofrooms { get; set; }
    }
}
