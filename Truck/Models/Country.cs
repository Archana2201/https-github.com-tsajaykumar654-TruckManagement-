using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class Country
    {
        public Country()
        {
            AppUsers = new HashSet<AppUser>();
            States = new HashSet<State>();
        }

        public int countryID { get; set; }
        public string Country_Name { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}
