using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class Language_Master
    {
        public Language_Master()
        {
            LanguageMapping_Masters = new HashSet<LanguageMapping_Master>();
            Navigation_Masters = new HashSet<Navigation_Master>();
        }

        public int Language_ID { get; set; }
        public string Language { get; set; }

        public virtual ICollection<LanguageMapping_Master> LanguageMapping_Masters { get; set; }
        public virtual ICollection<Navigation_Master> Navigation_Masters { get; set; }
    }
}
