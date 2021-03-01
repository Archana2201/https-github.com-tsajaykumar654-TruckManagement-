using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class Ecom_FavoriteListUserwise
    {
        public int Favorate_ID { get; set; }
        public int? FK_AppUser_Id { get; set; }
        public int? FK_Product_Id { get; set; }

        public virtual AppUser FK_AppUser { get; set; }
        public virtual Product FK_Product { get; set; }
    }
}
