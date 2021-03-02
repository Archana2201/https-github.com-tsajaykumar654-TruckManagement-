using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Setting
    {
        public int Settings_ID { get; set; }
        public int? FK_LangID { get; set; }
        public int? FK_ThemeID { get; set; }
        public int? Whatsapp_Notify { get; set; }
        public int? SMS_Notify { get; set; }
        public int? IVR_Notify { get; set; }

        public virtual Language FK_Lang { get; set; }
        public virtual Theme FK_Theme { get; set; }
    }
}
