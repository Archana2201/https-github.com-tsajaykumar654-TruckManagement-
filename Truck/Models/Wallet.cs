using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class Wallet
    {
        public int Wallet_ID { get; set; }
        public string Payment_Method { get; set; }
        public DateTime Payment_Date { get; set; }
    }
}
