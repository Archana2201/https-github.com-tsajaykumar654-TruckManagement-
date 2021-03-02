using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Ecom_ShoppingCart
    {
        public int ShoppingCart_ID { get; set; }
        public int? FK_AppUser_Id { get; set; }
        public DateTime Order_Date { get; set; }
        public int? FK_Product_Id { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public int? status { get; set; }
        public decimal? MRP { get; set; }

        public virtual AppUser FK_AppUser { get; set; }
        public virtual Product FK_Product { get; set; }
    }
}
