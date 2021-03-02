using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Team
    {
        public int Team_ID { get; set; }
        public int FK_userID { get; set; }
        public int FK_TeamRoleID { get; set; }
        public string Name { get; set; }
        public string Branch { get; set; }
        public string Mobile { get; set; }
        public string DP_Image { get; set; }
        public int? points { get; set; }
        public int? isDeleted { get; set; }
        public int? status { get; set; }
        public string refererCode { get; set; }
        public string whatsAppNo { get; set; }

        public virtual Teams_Role FK_TeamRole { get; set; }
        public virtual AppUser FK_user { get; set; }
    }
}
