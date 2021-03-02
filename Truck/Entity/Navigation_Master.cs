using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Navigation_Master
    {
        public int Navigation_ID { get; set; }
        public int? Fk_Language_ID { get; set; }
        public int? Fk_User_ID { get; set; }
        public string Keys { get; set; }
        public string Descriptions { get; set; }
        public string Icon { get; set; }

        public virtual Language_Master Fk_Language { get; set; }
        public virtual AppUser Fk_User { get; set; }
    }
}
