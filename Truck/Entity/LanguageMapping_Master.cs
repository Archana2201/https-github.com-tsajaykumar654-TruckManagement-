using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class LanguageMapping_Master
    {
        public int LanguageMapping_ID { get; set; }
        public int? Fk_Language_ID { get; set; }
        public int? Fk_Page_ID { get; set; }
        public int? Fk_Menu_ID { get; set; }
        public string Descriptions { get; set; }
        public string Keys { get; set; }
        public string Types { get; set; }
        public string Icon { get; set; }
        public bool? isdelete { get; set; }
        public bool? isedit { get; set; }

        public virtual Language_Master Fk_Language { get; set; }
        public virtual Menu_Master Fk_Menu { get; set; }
        public virtual Page_Master Fk_Page { get; set; }
    }
}
