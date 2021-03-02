using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Vehicle_Renewal_Info
    {
        public Vehicle_Renewal_Info()
        {
            Vehicle_Documents = new HashSet<Vehicle_Document>();
        }

        public int VehicleRenewalInfo_ID { get; set; }
        public int? FK_VehicleRenewal_ID { get; set; }
        public DateTime Registered_Date { get; set; }
        public string Vehicle_BackImage { get; set; }
        public string Insurance_Company { get; set; }
        public int? FK_Period_ID { get; set; }
        public string Vehicle_Name { get; set; }
        public string Vehicle_Number { get; set; }
        public int? Vehicle_Company_ID { get; set; }
        public int? Vehicle_Model_ID { get; set; }
        public string Vehicle_ModelNumber { get; set; }
        public bool? vehicle_type { get; set; }
        public string Vehicle_FrontImage { get; set; }
        public DateTime Expiry_Date { get; set; }

        public virtual Vehicle_Period FK_Period { get; set; }
        public virtual Vehicle_Renewal_Master FK_VehicleRenewal { get; set; }
        public virtual Vehicle_Company_Master Vehicle_Company { get; set; }
        public virtual Vehicle_Model_Master Vehicle_Model { get; set; }
        public virtual ICollection<Vehicle_Document> Vehicle_Documents { get; set; }
    }
}
