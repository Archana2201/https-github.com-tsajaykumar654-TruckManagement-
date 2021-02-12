using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class AppUserCredential
    {
        public int credentialID { get; set; }
        public int userID { get; set; }
        public string password { get; set; }

        public virtual AppUser user { get; set; }
    }
}
