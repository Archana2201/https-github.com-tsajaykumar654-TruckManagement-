using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class City
    {
        public City()
        {
            AppUsers = new HashSet<AppUser>();
            Areas = new HashSet<Area>();
            KYCs = new HashSet<KYC>();
        }

        public int city_ID { get; set; }
        public string City_Name { get; set; }
        public int? state_ID { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<KYC> KYCs { get; set; }
    }
}
