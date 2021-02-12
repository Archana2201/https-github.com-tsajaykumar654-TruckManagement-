using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class Menu_Master
    {
        public Menu_Master()
        {
            LanguageMapping_Masters = new HashSet<LanguageMapping_Master>();
        }

        public int Menu_ID { get; set; }
        public string Menu_Name { get; set; }

        public virtual ICollection<LanguageMapping_Master> LanguageMapping_Masters { get; set; }
    }
}
