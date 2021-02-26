using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class Vehicle_Model_Master
    {
        public Vehicle_Model_Master()
        {
            Vehicle_Renewal_Infos = new HashSet<Vehicle_Renewal_Info>();
        }

        public int VehicleModel_ID { get; set; }
        public string VehicleModel_Name { get; set; }

        public virtual ICollection<Vehicle_Renewal_Info> Vehicle_Renewal_Infos { get; set; }
    }
}
