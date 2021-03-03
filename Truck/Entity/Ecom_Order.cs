using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Ecom_Order
    {
        public Ecom_Order()
        {
            Ecom_Invoices = new HashSet<Ecom_Invoice>();
            Ecom_OrderItems = new HashSet<Ecom_OrderItem>();
            Ecom_Order_ShipmentDetails = new HashSet<Ecom_Order_ShipmentDetail>();
            Ecom_Shippings = new HashSet<Ecom_Shipping>();
        }

        public int Order_ID { get; set; }
        public int? FK_AppUser_Id { get; set; }
        public decimal? Order_SubTotal { get; set; }
        public decimal? Order_Tax { get; set; }
        public decimal? Order_Total { get; set; }
        public decimal? Order_Discount { get; set; }
        public decimal? Order_GrandTotal { get; set; }
        public DateTime? Order_Date { get; set; }
        public int? Fk_Shipping_id { get; set; }
        public string Payment_Status { get; set; }
        public string Razor_Order_Ids { get; set; }
        public int? OrderStatus { get; set; }
        public bool isCashOnDelivery { get; set; }
        public string CancelReason { get; set; }
        public decimal? Order_Shipping { get; set; }
        public string Order_Promo { get; set; }
        public string FK_Razor_Order_Id { get; set; }
        public string Payment_Details { get; set; }

        public virtual AppUser FK_AppUser { get; set; }
        public virtual Ecom_Shipping Fk_Shipping { get; set; }
        public virtual EcomOrderStatus_Master OrderStatusNavigation { get; set; }
        public virtual ICollection<Ecom_Invoice> Ecom_Invoices { get; set; }
        public virtual ICollection<Ecom_OrderItem> Ecom_OrderItems { get; set; }
        public virtual ICollection<Ecom_Order_ShipmentDetail> Ecom_Order_ShipmentDetails { get; set; }
        public virtual ICollection<Ecom_Shipping> Ecom_Shippings { get; set; }
    }
}
