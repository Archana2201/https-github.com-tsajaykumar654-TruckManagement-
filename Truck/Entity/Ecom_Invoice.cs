using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Ecom_Invoice
    {
        public int Invoice_Id { get; set; }
        public int? FK_AppUser_Id { get; set; }
        public int? FK_Shipment_Id { get; set; }
        public int? FK_Order_Id { get; set; }
        public DateTime Invoice_Date { get; set; }
        public int? FK_Payment_ID { get; set; }
        public string Invoice_Path { get; set; }

        public virtual AppUser FK_AppUser { get; set; }
        public virtual Ecom_Order FK_Order { get; set; }
    }
}
