using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Product_Category_Master
    {
        public Product_Category_Master()
        {
            Products = new HashSet<Product>();
        }

        public int ProductCategory_ID { get; set; }
        public string Product_Type { get; set; }
        public string Updated_By { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Last_Updated_Date { get; set; }
        public string ProdCategory_Path { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
