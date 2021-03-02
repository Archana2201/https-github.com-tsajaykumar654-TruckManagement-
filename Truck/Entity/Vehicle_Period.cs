using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Vehicle_Period
    {
        public Vehicle_Period()
        {
            Vehicle_Documents = new HashSet<Vehicle_Document>();
            Vehicle_Renewal_Infos = new HashSet<Vehicle_Renewal_Info>();
        }

        public int VehiclePeriod_ID { get; set; }
        public string VehiclePeriod_Type { get; set; }

        public virtual ICollection<Vehicle_Document> Vehicle_Documents { get; set; }
        public virtual ICollection<Vehicle_Renewal_Info> Vehicle_Renewal_Infos { get; set; }
    }
}
