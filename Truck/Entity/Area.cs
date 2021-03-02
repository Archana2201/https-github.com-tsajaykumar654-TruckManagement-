using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Area
    {
        public int Area_ID { get; set; }
        public string Area_Name { get; set; }
        public int? City_ID { get; set; }

        public virtual City City { get; set; }
    }
}
