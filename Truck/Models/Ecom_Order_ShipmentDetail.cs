using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class Ecom_Order_ShipmentDetail
    {
        public int oderShiptmentId { get; set; }
        public int orderId { get; set; }
        public string shipmentCompany { get; set; }
        public string shipmentID { get; set; }
        public DateTime shipmentDate { get; set; }
        public string shipmentURL { get; set; }
        public string attachment { get; set; }

        public virtual Ecom_Order order { get; set; }
    }
}
