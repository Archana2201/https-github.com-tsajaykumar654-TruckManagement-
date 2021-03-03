using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Ecom_Payment
    {
        public int Payment_ID { get; set; }
        public int? FK_AppUser_Id { get; set; }
        public int? FK_Invoice_Id { get; set; }
        public string Payment_Method { get; set; }
        public DateTime Payment_Date { get; set; }
        public int? FK_Order_Id { get; set; }

        public virtual AppUser FK_AppUser { get; set; }
        public virtual Ecom_Invoice FK_Invoice { get; set; }
    }
}
