using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Vehicle_Master
    {
        public Vehicle_Master()
        {
            Vehicle_Renewal_Infos = new HashSet<Vehicle_Renewal_Info>();
            Vehicle_Renewal_Masters = new HashSet<Vehicle_Renewal_Master>();
        }

        public int VehicleType_ID { get; set; }
        public string VehicleType_Name { get; set; }

        public virtual ICollection<Vehicle_Renewal_Info> Vehicle_Renewal_Infos { get; set; }
        public virtual ICollection<Vehicle_Renewal_Master> Vehicle_Renewal_Masters { get; set; }
    }
}
