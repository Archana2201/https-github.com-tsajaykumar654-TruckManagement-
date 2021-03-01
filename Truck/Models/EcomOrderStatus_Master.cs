using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class EcomOrderStatus_Master
    {
        public EcomOrderStatus_Master()
        {
            Ecom_Orders = new HashSet<Ecom_Order>();
        }

        public int OrderStatus_ID { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? Last_Updated_Date { get; set; }

        public virtual ICollection<Ecom_Order> Ecom_Orders { get; set; }
    }
}
