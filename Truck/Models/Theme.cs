using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class Theme
    {
        public Theme()
        {
            Settings = new HashSet<Setting>();
        }

        public int Themes_ID { get; set; }
        public string Themes_Type { get; set; }

        public virtual ICollection<Setting> Settings { get; set; }
    }
}
