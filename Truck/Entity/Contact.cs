using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Contact
    {
        public int Contact_ID { get; set; }
        public string Phone_Number { get; set; }
        public string Request_Type { get; set; }
        public string Message { get; set; }
    }
}
