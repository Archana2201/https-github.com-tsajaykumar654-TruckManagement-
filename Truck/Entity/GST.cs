using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class GST
    {
        public GST()
        {
            Products = new HashSet<Product>();
        }

        public int GST_ID { get; set; }
        public string GST_Value { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
