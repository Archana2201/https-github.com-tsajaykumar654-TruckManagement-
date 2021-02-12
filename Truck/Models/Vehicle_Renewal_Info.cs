using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class Vehicle_Renewal_Info
    {
        public int VehicleRenewalInfo_ID { get; set; }
        public int? FK_VehicleRenewal_ID { get; set; }
        public DateTime Registered_Date { get; set; }
        public DateTime Expiry_Date { get; set; }
        public string Vehicle_FrontImage { get; set; }
        public string Vehicle_BackImage { get; set; }
        public string Insurance_Company { get; set; }
        public int? FK_Period_ID { get; set; }
        public string Vehicle_Company { get; set; }
        public string Vehicle_Model { get; set; }
        public string Vehicle_Name { get; set; }
        public string Vehicle_Number { get; set; }

        public virtual Vehicle_Period FK_Period { get; set; }
        public virtual Vehicle_Renewal_Master FK_VehicleRenewal { get; set; }
    }
}
