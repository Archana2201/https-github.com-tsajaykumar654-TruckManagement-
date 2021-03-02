using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class State
    {
        public State()
        {
            AppUsers = new HashSet<AppUser>();
            KYCs = new HashSet<KYC>();
        }

        public int State_ID { get; set; }
        public string State_Name { get; set; }
        public int? FK_Country_ID { get; set; }

        public virtual Country FK_Country { get; set; }
        public virtual ICollection<AppUser> AppUsers { get; set; }
        public virtual ICollection<KYC> KYCs { get; set; }
    }
}
