using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class Page_Master
    {
        public Page_Master()
        {
            LanguageMapping_Masters = new HashSet<LanguageMapping_Master>();
        }

        public int Page_ID { get; set; }
        public string Page_Name { get; set; }

        public virtual ICollection<LanguageMapping_Master> LanguageMapping_Masters { get; set; }
    }
}
