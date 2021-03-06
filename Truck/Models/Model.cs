﻿using Microsoft.AspNetCore.Http;
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

    public class LoginModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string fcmID { get; set; }
        public string deviceID { get; set; }

        public string pin { get; set; }
    }

    public class AppUserForm
    {
        public string fullName { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }

        public DateTime? dateOfBirth { get; set; }
        public IFormFile profileImage { get; set; }
        public IFormFile pancard { get; set; }
        public IFormFile aadharcard { get; set; }
        public string gender { get; set; }
        public string Address { get; set; }
        public string company { get; set; }
        public string pincode { get; set; }
        public int? cityID { get; set; }
        public int? stateID { get; set; }
        public int? countryID { get; set; }
        public string Profileextension { get; set; }
        public string pancardextension { get; set; }
        public string aadharcardextension { get; set; }
    }
    public class AuthData
    {
        public string accessToken { get; set; }
        public string mobile { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string image { get; set; }
        public int userID { get; set; }
        public bool isAdmin { get; set; }
        public string role { get; set; }

        public int pin { get; set; }



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

    public class VehicleCompanyModel
    {

        public int VehicleCompany_ID { get; set; }

        public string VehicleCompany_Name { get; set; }


    }

    public class ProductCategoryMasterModel
    {


        public int ProductCategory_ID { get; set; }
        public string Product_Type { get; set; }
        public string Updated_By { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Last_Updated_Date { get; set; }
        public string ProdCategory_Path { get; set; }


    }
    public class ProductModel
    {
        public int productID { get; set; }
        public string Product_Name { get; set; }
        public int? FK_ProductCategory_Id { get; set; }
        public string Product_Description { get; set; }
        public string Photo_Path { get; set; }
        public decimal? MRP { get; set; }
        public string Product_Load { get; set; }
        public string Product_Manufacturer { get; set; }
        public string Product_CountryOrigin { get; set; }
        public string Product_CompanyName { get; set; }
        public string Product_CompanyAddress { get; set; }
        public string Product_CompanyContactNo { get; set; }
        public int? Product_Brand { get; set; }
        public int? Fk_User_ID { get; set; }
        public int? PercentDiscount { get; set; }
        public int? isActive { get; set; }
        public DateTime? createdDate { get; set; }


    }
    public class CartModel
    {
        public int userID { get; set; }
        public int? productID { get; set; }

        public int ShoppingCart_ID { get; set; }
        public int? FK_AppUser_Id { get; set; }
        public DateTime Order_Date { get; set; }
        public int? FK_Product_Id { get; set; }

        public string ProductName { get; set; }

        public decimal? Price { get; set; }
        public decimal? MRP { get; set; }
        public int? Quantity { get; set; }

        public string PhotoPath { get; set; }

        public int? isfreedelievry { get; set; }
        public int? PercentDiscount { get; set; }

        public string GST { get; set; }
        public int? Shipping_Charge { get; set; }

    }
    public class EcomOrderModel
    {
        public int Order_ID { get; set; }
        public int? FK_AppUser_Id { get; set; }
        public decimal? Order_SubTotal { get; set; }
        public decimal? Order_Tax { get; set; }
        public decimal? Order_Total { get; set; }
        public decimal? Order_Discount { get; set; }
        public decimal? Order_GrandTotal { get; set; }
        public DateTime? Order_Date { get; set; }
        public int? Fk_Shipping_id { get; set; }
        public string Payment_Status { get; set; }
        public string Razor_Order_Ids { get; set; }
        public int? OrderStatus { get; set; }
        public bool isCashOnDelivery { get; set; }
        public string CancelReason { get; set; }

    }
    public class EcomShippingModel
    {
        public int? FK_Order_Id { get; set; }
        public int Shipment_ID { get; set; }
        public int? FK_OrderItem_Id { get; set; }
        public int? FK_Product_Id { get; set; }
        public int? FK_AppUser_Id { get; set; }
        public string Shipment_Status { get; set; }
        public DateTime Created_Date { get; set; }
        public string FullName { get; set; }
        public string Shipping_Address { get; set; }
        public string Email_Address { get; set; }
        public string PhoneNos { get; set; }

        public string City { get; set; }

        public string PostCode { get; set; }

    }

    public class EcomOrderItemsViewModel
    {
        public EcomOrdersModel ecomorder { get; set; }
        public List<EcomOrdersItemsModel> ecomitemDetails { get; set; }
    }

    public class CancelOrderModel
    {
        public int? Order_ID { get; set; }
        public string Payment_Status { get; set; }
        public string Reason { get; set; }

    }

    public class EcomPaymentModel
    {
        public int Payment_ID { get; set; }
        public int? FK_AppUser_Id { get; set; }
        public int? FK_Invoice_Id { get; set; }
        public string Payment_Method { get; set; }
        public DateTime Payment_Date { get; set; }
    }

    public class EcomInvoiceModel
    {
        public int Invoice_Id { get; set; }
        public int? FK_AppUser_Id { get; set; }
        public int? FK_Shipment_Id { get; set; }
        public int? FK_Order_Id { get; set; }
        public DateTime Invoice_Date { get; set; }

    }
    public class SelectInvoice
    {

        public string path { get; set; }
    }


    public class EcomOrdersListModel
    {

        public int Order_ID { get; set; }
        public int? FK_AppUser_Id { get; set; }
        public string Order_Status { get; set; }
        public decimal? Order_SubTotal { get; set; }

        public decimal? Order_Tax { get; set; }
        public decimal? Order_Shipping { get; set; }
        public decimal? Order_Total { get; set; }
        public string Order_Promo { get; set; }
        public decimal? Order_Discount { get; set; }
        public decimal? Order_GrandTotal { get; set; }
        public DateTime? Order_Date { get; set; }

        public string Payment_Status { get; set; }
        public string Payment_Details { get; set; }
        public string FK_Razor_Order_Ids { get; set; }
        public EcomAddressDetailsModel ShippingAddress { get; set; }
        public List<EcomOrdersItemsModel> ecomitemDetails { get; set; }

    }
    public class EcomAddressDetailsModel
    {
        public int Shipment_ID { get; set; }

        public string ShippingAddress { get; set; }

        public string FullName { get; set; }

        public string ShipingStatus { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public string City { get; set; }

        public string PostCode { get; set; }
    }

    public class EcomOrdersModel
    {

        public int Order_ID { get; set; }
        public int? FK_AppUser_Id { get; set; }
        public string Order_Status { get; set; }
        public decimal? Order_SubTotal { get; set; }
        public decimal? Product_Discount { get; set; }
        public decimal? Order_Tax { get; set; }
        public decimal? Order_Shipping { get; set; }
        public decimal? Order_Total { get; set; }
        public string Order_Promo { get; set; }
        public decimal? Order_Discount { get; set; }
        public decimal? Order_GrandTotal { get; set; }
        public DateTime Order_Date { get; set; }

        public int? Fk_Shipping_id { get; set; }
        public string Payment_Status { get; set; }
        public string Payment_Details { get; set; }

        public string Fk_Razor_OrderIds { get; set; }

        public string Fk_RazorId { get; set; }

    }

    public class EcomOrdersItemsModel
    {

        public int OrderItems_ID { get; set; }
        public int? FK_Product_Id { get; set; }
        public int? FK_Order_Id { get; set; }
        public decimal? Order_Price { get; set; }
        public decimal? Product_Discount { get; set; }
        public int? Order_Quantity { get; set; }
        public decimal? Order_Tax { get; set; }
        public DateTime Order_Date { get; set; }

        public string ProductName { get; set; }

        public string PhotoPath { get; set; }

        public int? isfreedelievry { get; set; }
        public string PercentDiscount { get; set; }
        public int? Shipping_Charge { get; set; }


    }

    public class EcomFavorteModel
    {
        public int Favorate_ID { get; set; }
        public int? FK_AppUser_Id { get; set; }
        public int? FK_Product_Id { get; set; }
        public int? Fk_Brand_Id { get; set; }

    }
    public class EcomShoppingCartModel
    {
        public int ShoppingCart_ID { get; set; }
        public int? FK_AppUser_Id { get; set; }
        public DateTime Order_Date { get; set; }
        public int? FK_Product_Id { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public int? status { get; set; }
        public decimal? MRP { get; set; }


    }
    public class EcomTopicModel
    {
        public int? FK_Brandid { get; set; }

        public int? Topic_Id { get; set; }

        public string Topic_Name { get; set; }

        public string Topic_Description { get; set; }

        public bool Topic_CategoryYN { get; set; }

        public string Brand_Image { get; set; }
        public bool? ADDYN { get; set; }
        public bool? Slider { get; set; }
        public IFormFile filename { get; set; }
        public string extension { get; set; }

    }

    public class EcomTopicSubCatDetailsModel
    {
        public int FK_Topic_ID { get; set; }
        public int FK_Brandid { get; set; }
        public List<SubCatlist> prodlst { get; set; }
    }
    public class SubCatlist
    {
        public int value { get; set; }
    }
    public class EcomTopicProdDetailsModel
    {
        public int FK_Topic_ID { get; set; }
        public int FK_Brandid { get; set; }
        public List<Productlist> prodlst { get; set; }
    }
    public class Productlist
    {
        public int value { get; set; }
    }
    public class VehicleModel
    {

        public int VehicleModel_ID { get; set; }

        public string VehicleModel_Name { get; set; }


    }

    public class VehicleCreateModel
    {
        public int VehicleRenewalInfo_ID { get; set; }
        public int? FK_VehicleRenewal_ID { get; set; }
       
        public string Vehicle_ModelNumber { get; set; }

       
        public string Vehicle_Name { get; set; }
        public string Vehicle_Number { get; set; }
        public int? Vehicle_Company_ID { get; set; }
        public int? Vehicle_Model_ID { get; set; }

        public bool? vehicle_type { get; set; }
    }
    public class VehicleInfoModel
    {
        public int VehicleRenewalInfo_ID { get; set; }
        public int? FK_VehicleRenewal_ID { get; set; }
        public DateTime Registered_Date { get; set; }
        public DateTime Expiry_Date { get; set; }
        public string Vehicle_FrontImage { get; set; }
        public string Vehicle_BackImage { get; set; }
        public string Insurance_Company { get; set; }
        public string Vehicle_ModelNumber { get; set; }

        public int? FK_Period_ID { get; set; }
        public string Vehicle_Name { get; set; }
        public string Vehicle_Number { get; set; }
        public int? Vehicle_Company_ID { get; set; }
        public int? Vehicle_Model_ID { get; set; }

        public bool? vehicle_type { get; set; }
    }

    public class TeamModel
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

        public string Roles { get; set; }
    }

    public class TeamModelList
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

        public List<string> Roles { get; set; }
    }


    public class VehicleDocumentModel
    {
        public int VehicleDocuments_ID { get; set; }
        public int? FK_VehicleRenewal_ID { get; set; }
        public int? FK_VehicleRenewalinfo_ID { get; set; }
        public DateTime Registered_Date { get; set; }
        public DateTime Expiry_Date { get; set; }
        public string Vehicle_FrontImage { get; set; }
        public string Vehicle_BackImage { get; set; }
        public string Insurance_Company { get; set; }
        public int? FK_Period_ID { get; set; }

        public IFormFile frontfilename { get; set; }

        public IFormFile backfilename { get; set; }
        public string extension { get; set; }

    }

    public class InsuranceRenewedModel
    {
        public int InsuranceRenewed_ID { get; set; }
        public DateTime Registered_Date { get; set; }
        public DateTime Expiry_Date { get; set; }
        public string Vehicle_FrontImage { get; set; }
        public string Vehicle_BackImage { get; set; }
        public string Insurance_Company { get; set; }

        public IFormFile filename { get; set; }

        public IFormFile backfilename { get; set; }
        public string extension { get; set; }
        public string backextension { get; set; }

    }

    public class VehicleDocumentModelList
    {
        public List<VehicleDocModel> Npermit { get; set; }
        public List<VehicleDocModel> ProductsOrdered { get; set; }
        public List<VehicleDocModel> RoadTax { get; set; }
        public List<VehicleDocModel> Insurance { get; set; }
    }

    public class VehicleDocModel
    {

        public string Vehicle_FrontImage { get; set; }

        public string Vehiclename { get; set; }

    }
    public class DocList
    {
        public string year { get; set; }
        public List<ListDoc> doclists { get; set; }
    }

    public class ListDoc
    {
        public string images { get; set; }

    }
    public class ContactModel
    {
        public int Contact_ID { get; set; }
        public string Phone_Number { get; set; }
        public string Request_Type { get; set; }
        public string Message { get; set; }
    }
    public class SettingModel
    {
        public int Settings_ID { get; set; }
        public int? FK_LangID { get; set; }
        public int? FK_ThemeID { get; set; }
        public int? Whatsapp_Notify { get; set; }
        public int? SMS_Notify { get; set; }
        public int? IVR_Notify { get; set; }


    }

    public class TopicDetailsModel
    {
        public int? Topic_ID { get; set; }
        public string Topic_Name { get; set; }

        public string Topic_Description { get; set; }



        public List<EcomTopicCategory> ltsSubCategory { get; set; }




        public List<EcomTopicProduct> lstProduct { get; set; }



    }
    public class EcommerceTopicsModel
    {

        public int Topic_Id { get; set; }

        public string Topic_Name { get; set; }

        public string Topic_Description { get; set; }

        public bool Topic_CategoryYN { get; set; }
        public string Brand_Image { get; set; }
        public bool? ADDYN { get; set; }
        public bool? Slider { get; set; }
        public List<EcommerceCategoryModel> Names { get; set; }
    }
    public class EcommerceCategoryModel
    {

        public int? ID { get; set; }

        public int? FK_Brandid { get; set; }

        public int? FK_Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public bool YN { get; set; }
    }
    public class EcomTopicCategory
    {
        public int? FK_SubCategory { get; set; }
        public bool TopicDetails_FrontYN { get; set; }

        public string Product_Sub_Category { get; set; }
    }

    public class EcomTopicProduct
    {
        public int? FK_Productid { get; set; }
        public bool TopicDetails_FrontYN { get; set; }

        public string name { get; set; }
    }
    public  class AppUserModel
    {
        

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

      
    }


}
