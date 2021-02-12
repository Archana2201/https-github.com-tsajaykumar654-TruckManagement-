using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class Product
    {
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

        public virtual Product_Category_Master FK_ProductCategory { get; set; }
        public virtual AppUser Fk_User { get; set; }
    }
}
