using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class Teams_Role
    {
        public Teams_Role()
        {
            Teams = new HashSet<Team>();
        }

        public int Team_RoleID { get; set; }
        public string Team_RoleType { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
