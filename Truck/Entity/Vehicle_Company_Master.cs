using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Vehicle_Company_Master
    {
        public Vehicle_Company_Master()
        {
            Vehicle_Renewal_Infos = new HashSet<Vehicle_Renewal_Info>();
        }

        public int VehicleCompany_ID { get; set; }
        public string VehicleCompany_Name { get; set; }

        public virtual ICollection<Vehicle_Renewal_Info> Vehicle_Renewal_Infos { get; set; }
    }
}
