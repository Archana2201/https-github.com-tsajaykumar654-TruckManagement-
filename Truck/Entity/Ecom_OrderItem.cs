using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Ecom_OrderItem
    {
        public Ecom_OrderItem()
        {
            Ecom_Shippings = new HashSet<Ecom_Shipping>();
        }

        public int OrderItems_ID { get; set; }
        public int? FK_Product_Id { get; set; }
        public int? FK_Order_Id { get; set; }
        public decimal? Order_Price { get; set; }
        public decimal? Product_Discount { get; set; }
        public int? Order_Quantity { get; set; }
        public decimal? Order_Tax { get; set; }
        public DateTime Order_Date { get; set; }

        public virtual Ecom_Order FK_Order { get; set; }
        public virtual Product FK_Product { get; set; }
        public virtual ICollection<Ecom_Shipping> Ecom_Shippings { get; set; }
    }
}
