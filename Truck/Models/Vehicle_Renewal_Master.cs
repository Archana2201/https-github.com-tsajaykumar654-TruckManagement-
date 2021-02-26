using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class Vehicle_Renewal_Master
    {
        public Vehicle_Renewal_Master()
        {
            Vehicle_Documents = new HashSet<Vehicle_Document>();
            Vehicle_Renewal_Infos = new HashSet<Vehicle_Renewal_Info>();
        }

        public int VehicleRenewal_ID { get; set; }
        public string VehicleRenewal_Name { get; set; }

        public virtual ICollection<Vehicle_Document> Vehicle_Documents { get; set; }
        public virtual ICollection<Vehicle_Renewal_Info> Vehicle_Renewal_Infos { get; set; }
    }
}
