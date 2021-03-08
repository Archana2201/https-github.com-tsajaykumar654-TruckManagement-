using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Insurance_Renewed
    {
        public int InsuranceRenewed_ID { get; set; }
        public DateTime Registered_Date { get; set; }
        public DateTime Expiry_Date { get; set; }
        public string Vehicle_FrontImage { get; set; }
        public string Vehicle_BackImage { get; set; }
        public string Insurance_Company { get; set; }
    }
}
