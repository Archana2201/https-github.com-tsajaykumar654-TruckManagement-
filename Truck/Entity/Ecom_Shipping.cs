using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Ecom_Shipping
    {
        public Ecom_Shipping()
        {
            Ecom_Orders = new HashSet<Ecom_Order>();
        }

        public int Shipment_ID { get; set; }
        public int? FK_Order_Id { get; set; }
        public int? FK_OrderItem_Id { get; set; }
        public int? FK_Product_Id { get; set; }
        public int? FK_AppUser_Id { get; set; }
        public string Shipment_Status { get; set; }
        public DateTime Created_Date { get; set; }
        public string FullName { get; set; }
        public string Shipping_Address { get; set; }
        public string Email_Address { get; set; }
        public string PhoneNos { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }

        public virtual AppUser FK_AppUser { get; set; }
        public virtual Ecom_Order FK_Order { get; set; }
        public virtual Ecom_OrderItem FK_OrderItem { get; set; }
        public virtual Product FK_Product { get; set; }
        public virtual ICollection<Ecom_Order> Ecom_Orders { get; set; }
    }
}
