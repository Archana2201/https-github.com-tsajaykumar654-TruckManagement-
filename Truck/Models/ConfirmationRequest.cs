using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class ConfirmationRequest
    {
        public int confirmationID { get; set; }
        public int targetID { get; set; }
        public DateTime resetTime { get; set; }
        public int confirmType { get; set; }
        public string code { get; set; }
    }
}
