using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Language
    {
        public Language()
        {
            Settings = new HashSet<Setting>();
        }

        public int Lang_ID { get; set; }
        public string Lang_Type { get; set; }

        public virtual ICollection<Setting> Settings { get; set; }
    }
}
