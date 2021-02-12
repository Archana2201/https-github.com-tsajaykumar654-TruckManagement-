using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Truck.Models
{
    public class Model
    {
    }

    public class LangMenuPageModel
    {
        public string LanguageName { get; set; }
        public List<PageModel> Pages { get; set; }
    }

    public class PageModel
    {
        public string pagename { get; set; }
      
        public List<MenuModel> menus { get; set; }
    }

    public class MenuModel
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }

    }

    public class PageMasterModel
    {
        public string Page_Name { get; set; }
    }

    public class Select
    {
        public string name { get; set; }
        public int value { get; set; }

    }
    public class LangMenuModel
    {
        public List<LanguageMappingMasterModel> langmap { get; set; }
    }
    public class LanguageMappingMasterModel
    {
        
        public int? Fk_Language_ID { get; set; }
        public int? Fk_Page_ID { get; set; }
        public int? Fk_Menu_ID { get; set; }
        public string Keys { get; set; }
        public string Descriptions { get; set; }
        public string Types { get; set; }
    }

    public class NavigationModel
    {
        public int? Fk_Language_ID { get; set; }
        public int? Fk_User_ID { get; set; }
        public string Keys { get; set; }
        public string Descriptions { get; set; }
        public string Icon { get; set; }
    }
    public class MenuMasterModel
    {
        public string Menu_Name { get; set; }
    }
    public class LanguageMasterModel
    {

        public string Language { get; set; }
    }
    public class NotificationRequest
    {
        public string message { get; set; }
        public int id { get; set; }
        public string joinUrl { get; set; }
        public DateTime dateTime { get; set; }
        public int type { get; set; }
        public int outletID { get; set; }
        public int userID { get; set; }
    }

    public class AppUserRegistration
    {
        public string fullName { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string userName { get; set; }
        public string ReferalCode { get; set; }
    }
}
