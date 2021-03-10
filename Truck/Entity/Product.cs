using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Product
    {
        public Product()
        {
            Ecom_FavoriteListUserwises = new HashSet<Ecom_FavoriteListUserwise>();
            Ecom_OrderItems = new HashSet<Ecom_OrderItem>();
            Ecom_Shippings = new HashSet<Ecom_Shipping>();
            Ecom_ShoppingCarts = new HashSet<Ecom_ShoppingCart>();
            Ecom_TopicDetails_Products = new HashSet<Ecom_TopicDetails_Product>();
        }

        public int productID { get; set; }
        public string Product_Name { get; set; }
        public int? FK_ProductCategory_Id { get; set; }
        public string Product_Description { get; set; }
        public string Photo_Path { get; set; }
        public decimal? MRP { get; set; }
        public string Product_Load { get; set; }
        public string Product_Manufacturer { get; set; }
        public string Product_CountryOrigin { get; set; }
        public string Product_CompanyName { get; set; }
        public string Product_CompanyAddress { get; set; }
        public string Product_CompanyContactNo { get; set; }
        public int? Product_Brand { get; set; }
        public int? Fk_User_ID { get; set; }
        public int? PercentDiscount { get; set; }
        public int? isActive { get; set; }
        public DateTime? createdDate { get; set; }
        public int? Qty { get; set; }
        public decimal? SP { get; set; }
        public int? FK_GST { get; set; }
        public int? Shipping_Charge { get; set; }

        public virtual GST FK_GSTNavigation { get; set; }
        public virtual Product_Category_Master FK_ProductCategory { get; set; }
        public virtual AppUser Fk_User { get; set; }
        public virtual ICollection<Ecom_FavoriteListUserwise> Ecom_FavoriteListUserwises { get; set; }
        public virtual ICollection<Ecom_OrderItem> Ecom_OrderItems { get; set; }
        public virtual ICollection<Ecom_Shipping> Ecom_Shippings { get; set; }
        public virtual ICollection<Ecom_ShoppingCart> Ecom_ShoppingCarts { get; set; }
        public virtual ICollection<Ecom_TopicDetails_Product> Ecom_TopicDetails_Products { get; set; }
    }
}
