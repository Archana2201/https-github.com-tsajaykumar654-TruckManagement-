using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class Audit
    {
        public long auditID { get; set; }
        public int userID { get; set; }
        public string fcmID { get; set; }
        public string deviceID { get; set; }
        public DateTime createdDate { get; set; }
    }
}
