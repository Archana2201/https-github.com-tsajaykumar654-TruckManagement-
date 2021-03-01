using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Models
{
    public partial class AppUser
    {
        public AppUser()
        {
            AppUserCredentials = new HashSet<AppUserCredential>();
            Ecom_FavoriteListUserwises = new HashSet<Ecom_FavoriteListUserwise>();
            Ecom_Invoices = new HashSet<Ecom_Invoice>();
            Ecom_Orders = new HashSet<Ecom_Order>();
            Ecom_Shippings = new HashSet<Ecom_Shipping>();
            Ecom_ShoppingCarts = new HashSet<Ecom_ShoppingCart>();
            Navigation_Masters = new HashSet<Navigation_Master>();
            Products = new HashSet<Product>();
            Teams = new HashSet<Team>();
        }

        public int userID { get; set; }
        public string fullName { get; set; }
        public string userName { get; set; }
        public string gender { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string dpPath { get; set; }
        public string AadharCard { get; set; }
        public string PanCard { get; set; }
        public string Address { get; set; }
        public string pincode { get; set; }
        public int? cityID { get; set; }
        public int? stateID { get; set; }
        public int? countryID { get; set; }
        public int isActive { get; set; }
        public int IsDeleted { get; set; }
        public DateTime createdDate { get; set; }
        public int? referalStatus { get; set; }
        public int? referalUserId { get; set; }
        public int? referalTeamId { get; set; }
        public int? verified { get; set; }
        public string OTP { get; set; }
        public DateTime? OTPExpireTime { get; set; }
        public string Pin { get; set; }
        public string company { get; set; }

        public virtual City city { get; set; }
        public virtual Country country { get; set; }
        public virtual State state { get; set; }
        public virtual ICollection<AppUserCredential> AppUserCredentials { get; set; }
        public virtual ICollection<Ecom_FavoriteListUserwise> Ecom_FavoriteListUserwises { get; set; }
        public virtual ICollection<Ecom_Invoice> Ecom_Invoices { get; set; }
        public virtual ICollection<Ecom_Order> Ecom_Orders { get; set; }
        public virtual ICollection<Ecom_Shipping> Ecom_Shippings { get; set; }
        public virtual ICollection<Ecom_ShoppingCart> Ecom_ShoppingCarts { get; set; }
        public virtual ICollection<Navigation_Master> Navigation_Masters { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
