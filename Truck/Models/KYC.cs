using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class KYC
    {
        public int Kyc_ID { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public string Address { get; set; }
        public string pincode { get; set; }
        public int? cityID { get; set; }
        public int? stateID { get; set; }
        public int? countryID { get; set; }
        public string dpPath { get; set; }
        public string AadharCard { get; set; }
        public string PanCard { get; set; }
        public int isActive { get; set; }
        public int IsDeleted { get; set; }
        public DateTime createdDate { get; set; }
        public int? verified { get; set; }
        public int? Fk_User_Id { get; set; }

        public virtual City city { get; set; }
        public virtual State state { get; set; }
    }
}
