using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Truck.Models;
using Truck.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Truck.Entity;

namespace Truck.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v2")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private readonly TruckContext _context;
        private readonly IRepos _repos;
        private readonly IStorage _storage;
        public MobileController(TruckContext context, IRepos repos, IStorage storage)
        {
            _context = context;
            _repos = repos;
            _storage = storage;

        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ApiResponse<IEnumerable<LangMenuPageModel>>>> GetLanguageData(int langid)
        {
            try
            {
                var query = await _context.LanguageMapping_Masters.Where(w => w.Fk_Language_ID == langid).Select(s => new LangMenuPageModel
                {
                    LanguageName = s.Fk_Language.Language,
                    Pages = _context.LanguageMapping_Masters.Where(w => w.Fk_Language_ID == langid).Select(i => new PageModel
                    {
                        pagename = i.Fk_Page.Page_Name,
                        menus = _context.LanguageMapping_Masters.Where(w => w.Fk_Language_ID == langid).Select(j => new MenuModel
                        {
                            Key = j.Keys,
                            Description = j.Descriptions,
                            Type = j.Types,
                            Icon = j.Icon,
                        }).ToList()
                    }).ToList(),
                }).ToListAsync();
                return new ApiResponse<IEnumerable<LangMenuPageModel>> { code = 1, message = "Successfull", data = query };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<LangMenuPageModel>> { code = 0, message = ex.Message, data = null };
            }
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<List<NavigationModel>>> GetNavigation(int langid)
        {
            return await _context.Navigation_Masters.Where(w => w.Fk_Language_ID == langid).Select(x => new NavigationModel
            {
                Keys = x.Keys,
                Descriptions = x.Descriptions,
                Icon = x.Icon,
            }).ToListAsync();
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<AppUserModel>>> UpdateUserProfile([FromForm] AppUserForm form)
        {
            if (string.IsNullOrEmpty(form.email) || string.IsNullOrEmpty(form.fullName) || string.IsNullOrEmpty(form.mobile))
            {
                return new ApiResponse<AppUserModel> { code = 0, message = "Null Values" };
            }
            var appUser = await _context.AppUsers.FindAsync(_repos.mobile);
            if (form.profileImage.Length > 0)
            {
                if (appUser.dpPath != "/images/profile-picture/default.png")
                {
                    await _storage.DeleteIfExists(appUser.dpPath);
                }

                string dpPath = await _storage.Save(form.profileImage, "/profile-picture", form.Profileextension);
                appUser.dpPath = dpPath;
            }
            if (form.aadharcard.Length > 0)
            {
                if (appUser.AadharCard != "/images/profile-picture/default.png")
                {
                    await _storage.DeleteIfExists(appUser.AadharCard);
                }
                string straadharCard = await _storage.Save(form.aadharcard, "/profile-picture", form.aadharcardextension);
                appUser.AadharCard = straadharCard;

            }
            if (form.pancard.Length > 0)
            {
                if (appUser.PanCard != "/images/profile-picture/default.png")
                {
                    await _storage.DeleteIfExists(appUser.PanCard);
                }
                //appUser.PanCard = await _storage.Save(form.pancard, "/profile-picture");

                string strPanCard = await _storage.Save(form.pancard, "/profile-picture", form.pancardextension);
                appUser.PanCard = strPanCard;
            }

            appUser.fullName = form.fullName;
            appUser.company = form.company;
            appUser.email = form.email;
            appUser.stateID = form.stateID;
            appUser.cityID = form.cityID;
            appUser.Address = form.Address;
            appUser.countryID = form.countryID;
            appUser.gender = form.gender;
            appUser.pincode = form.pincode;
            appUser.dateOfBirth = form.dateOfBirth;


            _context.Entry(appUser).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ApiResponse<AppUserModel> { code = 0, message = "Error" };
            }
            return new ApiResponse<AppUserModel> { code = 1, message = "Success" };
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<ApiResponse<List<Select>>>> LanguageDDL()
        {
            var list = await _context.Language_Masters.Select(x => new Select
            {
                name = x.Language,
                value = x.Language_ID
            }).ToListAsync();
            return new ApiResponse<List<Select>> { code = 1, data = list };
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ApiResponse<List<Select>>>> VehicleCompanyDDL()
        {
            var list = await _context.Vehicle_Company_Masters.Select(x => new Select
            {
                name = x.VehicleCompany_Name,
                value = x.VehicleCompany_ID
            }).ToListAsync();
            return new ApiResponse<List<Select>> { code = 1, data = list };
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ApiResponse<List<Select>>>> VehicleModelDDL()
        {
            var list = await _context.Vehicle_Model_Masters.Select(x => new Select
            {
                name = x.VehicleModel_Name,
                value = x.VehicleModel_ID
            }).ToListAsync();
            return new ApiResponse<List<Select>> { code = 1, data = list };
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ApiResponse<List<Select>>>> UserVehicleModelDDL()
        {
            var list = await _context.Vehicle_Renewal_Infos.Select(x => new Select
            {
                name = x.Vehicle_Number,
                value = x.VehicleRenewalInfo_ID
            }).ToListAsync();
            return new ApiResponse<List<Select>> { code = 1, data = list };
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<ApiResponse<List<Select>>>> VehicleRenewalTypeDDL()
        {
            var list = await _context.Vehicle_Renewal_Masters.Select(x => new Select
            {
                name = x.VehicleRenewal_Name,
                value = x.VehicleRenewal_ID
            }).ToListAsync();
            return new ApiResponse<List<Select>> { code = 1, data = list };
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ApiResponse<List<Select>>>> VehicleInsuranceDDL()
        {
            var list = await _context.Insurances.Select(x => new Select
            {
                name = x.Insurance_Type,
                value = x.Insurance_ID
            }).ToListAsync();
            return new ApiResponse<List<Select>> { code = 1, data = list };
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ApiResponse<List<Select>>>> VehiclePeriodDDL()
        {
            var list = await _context.Vehicle_Periods.Select(x => new Select
            {
                name = x.VehiclePeriod_Type,
                value = x.VehiclePeriod_ID
            }).ToListAsync();
            return new ApiResponse<List<Select>> { code = 1, data = list };
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<VehicleCreateModel>>> CreateOrUpdateVehicle(VehicleCreateModel model)
        {
            try
            {
                var vehicle = await _context.Vehicle_Renewal_Infos.Where(x => x.VehicleRenewalInfo_ID == model.VehicleRenewalInfo_ID).FirstOrDefaultAsync();
                Vehicle_Renewal_Info vehicleinfo = (vehicle == null) ? new Vehicle_Renewal_Info() : vehicle;

                model.Vehicle_Number = model.Vehicle_Number.TrimStart().TrimEnd();

                vehicleinfo.Vehicle_Model_ID = (model.Vehicle_Model_ID == 0) ? vehicleinfo.Vehicle_Model_ID : model.Vehicle_Model_ID;
                vehicleinfo.Vehicle_Number = (string.IsNullOrEmpty(model.Vehicle_Number)) ? vehicleinfo.Vehicle_Number : model.Vehicle_Number;
                vehicleinfo.Vehicle_ModelNumber = (string.IsNullOrEmpty(model.Vehicle_ModelNumber)) ? vehicleinfo.Vehicle_ModelNumber : model.Vehicle_ModelNumber;
                vehicleinfo.Vehicle_Company_ID = (model.Vehicle_Company_ID == 0) ? vehicleinfo.Vehicle_Company_ID : model.Vehicle_Company_ID;
                vehicleinfo.vehicle_type = model.vehicle_type;//0--Personal n 1--Corporate
                if (vehicle == null)
                {
                    _context.Vehicle_Renewal_Infos.Add(vehicleinfo);
                }

                await _context.SaveChangesAsync();

                return new ApiResponse<VehicleCreateModel> { code = 1, message = "Successfull" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<VehicleCreateModel> { code = 0, message = ex.Message, data = null };
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<int>>> UploadDocuments([FromForm] VehicleDocumentModel model)
        {
            try
            {
                var vehicle = await _context.Vehicle_Documents.Where(x => x.VehicleDocuments_ID == model.VehicleDocuments_ID).FirstOrDefaultAsync();
                Vehicle_Document vehicledocs = (vehicle == null) ? new Vehicle_Document() : vehicle;
                if (model.frontfilename.Length > 0)
                {
                    if (!string.IsNullOrEmpty(model.Vehicle_FrontImage))
                    {
                        if (vehicledocs.Vehicle_FrontImage != "/images/profile-picture/default.png")
                        {
                            await _storage.DeleteIfExists(vehicledocs.Vehicle_FrontImage);
                        }
                        string frontFullPath = await _storage.Save(model.frontfilename, "/Vehicle - Docs", model.extension);
                        vehicledocs.Vehicle_FrontImage = frontFullPath;
                    }

                    if (!string.IsNullOrEmpty(model.Vehicle_BackImage))
                    {
                        if (vehicledocs.Vehicle_BackImage != "/images/profile-picture/default.png")
                        {
                            await _storage.DeleteIfExists(vehicledocs.Vehicle_BackImage);
                        }
                        string backFullPath = await _storage.Save(model.backfilename, "/Vehicle - Docs", model.extension);
                        vehicledocs.Vehicle_BackImage = backFullPath;
                    }
                }

                vehicledocs.Registered_Date = model.Registered_Date;
                vehicledocs.Expiry_Date = model.Expiry_Date;
                vehicledocs.FK_VehicleRenewal_ID = model.FK_VehicleRenewal_ID;
                vehicledocs.Insurance_Company = (string.IsNullOrEmpty(model.Insurance_Company)) ? vehicledocs.Insurance_Company : model.Insurance_Company;
                vehicledocs.FK_Period_ID = (model.FK_Period_ID == 0) ? vehicledocs.FK_Period_ID : model.FK_Period_ID;
                vehicledocs.FK_VehicleRenewal_ID = (model.FK_VehicleRenewal_ID == 0) ? vehicledocs.FK_VehicleRenewal_ID : model.FK_VehicleRenewal_ID;

                if (vehicle == null)
                {
                    _context.Vehicle_Documents.Add(vehicledocs);
                }

                await _context.SaveChangesAsync();

                return new ApiResponse<int> { code = 1, message = "Successfull" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<int> { code = 0, message = ex.Message };
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<SettingModel>>> CreateOrUpdateSettings(SettingModel model)
        {
            try
            {
                var settings = await _context.Settings.Where(x => x.Settings_ID == model.Settings_ID).FirstOrDefaultAsync();
                Setting Settinmodel = (settings == null) ? new Setting() : settings;



                Settinmodel.FK_LangID = (model.FK_LangID == 0) ? Settinmodel.FK_LangID : model.FK_LangID;
                Settinmodel.FK_ThemeID = (model.FK_ThemeID == 0) ? Settinmodel.FK_ThemeID : model.FK_ThemeID;
                Settinmodel.Whatsapp_Notify = (model.Whatsapp_Notify == 0) ? Settinmodel.Whatsapp_Notify : model.Whatsapp_Notify;
                Settinmodel.SMS_Notify = (model.SMS_Notify == 0) ? Settinmodel.SMS_Notify : model.SMS_Notify;
                Settinmodel.IVR_Notify = (model.IVR_Notify == 0) ? Settinmodel.IVR_Notify : model.IVR_Notify;

                if (settings == null)
                {
                    _context.Settings.Add(Settinmodel);
                }

                await _context.SaveChangesAsync();

                return new ApiResponse<SettingModel> { code = 1, message = "Successfull" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<SettingModel> { code = 0, message = ex.Message, data = null };
            }
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<ContactModel>>> CreateContacts(ContactModel model)
        {
            try
            {
                var con = await _context.Contacts.Where(x => x.Contact_ID == model.Contact_ID).FirstOrDefaultAsync();
                Contact contacts = (con == null) ? new Contact() : con;

                contacts.Phone_Number = (string.IsNullOrEmpty(model.Phone_Number)) ? contacts.Phone_Number : model.Phone_Number;
                contacts.Request_Type = (string.IsNullOrEmpty(model.Request_Type)) ? contacts.Request_Type : model.Request_Type;
                contacts.Message = (string.IsNullOrEmpty(model.Message)) ? contacts.Message : model.Message;

                if (con == null)
                {
                    _context.Contacts.Add(contacts);
                }

                await _context.SaveChangesAsync();

                return new ApiResponse<ContactModel> { code = 1, message = "Successfull" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ContactModel> { code = 0, message = ex.Message, data = null };
            }
        }



        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<VehicleInfoModel>>> VehicleList()
        {
            return await _context.Vehicle_Renewal_Infos.Select(x => new VehicleInfoModel
            {
                VehicleRenewalInfo_ID = x.VehicleRenewalInfo_ID,
                Vehicle_Number = x.Vehicle_Number,
                Vehicle_Model_ID = x.Vehicle_Model_ID,
                Vehicle_ModelNumber = x.Vehicle_ModelNumber,
                Vehicle_Company_ID = x.Vehicle_Company_ID,
                vehicle_type = x.vehicle_type


            }).ToListAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<VehicleInfoModel>>> VehicleByID(int vehicleid)
        {
            return await _context.Vehicle_Renewal_Infos.Where(w => w.VehicleRenewalInfo_ID == vehicleid).Select(x => new VehicleInfoModel
            {
                VehicleRenewalInfo_ID = x.VehicleRenewalInfo_ID,
                Vehicle_Number = x.Vehicle_Number,
                Vehicle_Model_ID = x.Vehicle_Model_ID,
                Vehicle_ModelNumber = x.Vehicle_ModelNumber,
                Vehicle_Company_ID = x.Vehicle_Company_ID,
                vehicle_type = x.vehicle_type
            }).ToListAsync();
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<InsuranceRenewedModel>>> UploadInsuranceRenewal(InsuranceRenewedModel model)
        {
            try
            {
                var vehicle = await _context.Insurance_Reneweds.Where(x => x.InsuranceRenewed_ID == model.InsuranceRenewed_ID).FirstOrDefaultAsync();
                Insurance_Renewed vehicledocs = (vehicle == null) ? new Insurance_Renewed() : vehicle;



                if (!string.IsNullOrEmpty(model.Vehicle_FrontImage))
                {
                    if (vehicledocs.Vehicle_FrontImage != "/images/profile-picture/default.png")
                    {
                        await _storage.DeleteIfExists(vehicledocs.Vehicle_FrontImage);
                    }
                    string frontFullPath = await _storage.Save(model.filename, "/Vehicle - Docs", model.extension);
                    vehicledocs.Vehicle_FrontImage = frontFullPath;
                }


                if (!string.IsNullOrEmpty(model.Vehicle_BackImage))
                {
                    if (vehicledocs.Vehicle_BackImage != "/images/profile-picture/default.png")
                    {
                        await _storage.DeleteIfExists(vehicledocs.Vehicle_BackImage);
                    }
                    string backFullPath = await _storage.Save(model.backfilename, "/Vehicle - Docs", model.backextension);
                    vehicledocs.Vehicle_BackImage = backFullPath;
                }



                vehicledocs.Registered_Date = model.Registered_Date;
                vehicledocs.Expiry_Date = model.Expiry_Date;

                if (vehicle == null)
                {
                    _context.Insurance_Reneweds.Add(vehicledocs);
                }

                await _context.SaveChangesAsync();

                return new ApiResponse<InsuranceRenewedModel> { code = 1, message = "Successfull" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<InsuranceRenewedModel> { code = 0, message = ex.Message, data = null };
            }
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<InsuranceRenewedModel>>> RenewalList()
        {
            return await _context.Insurance_Reneweds.Select(x => new InsuranceRenewedModel
            {
                InsuranceRenewed_ID = x.InsuranceRenewed_ID,
                Registered_Date = x.Registered_Date,
                Expiry_Date = x.Expiry_Date,
                Vehicle_FrontImage = x.Vehicle_FrontImage,
                Vehicle_BackImage = x.Vehicle_BackImage,
                Insurance_Company = x.Insurance_Company
            }).ToListAsync();
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<ApiResponse<IEnumerable<DocList>>>> GetDocListYearwise()
        {
            try
            {
                var query = await _context.Vehicle_Documents.Where(w => w.FK_APPUSERID == _repos.UserID).Select(s => new DocList
                {
                    year = s.Registered_Date.ToString("yyyy"),

                    doclists = _context.Vehicle_Documents.Where(a => a.FK_APPUSERID == _repos.UserID).Select(i => new ListDoc
                    {

                        images = i.Vehicle_FrontImage,

                    }).ToList(),
                }).ToListAsync();
                return new ApiResponse<IEnumerable<DocList>> { code = 1, message = "Successfull", data = query };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<DocList>> { code = 0, message = ex.Message, data = null };
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<AppUserModel>>> GetProfile()
        {
            return await _context.AppUsers.Where(w => w.userID == _repos.UserID).Select(x => new AppUserModel
            {
                fullName = x.fullName,
                company = x.company,
                email = x.email,
                stateID = x.stateID,
                cityID = x.cityID,
                Address = x.Address,
                countryID = x.countryID,
                gender = x.gender,
                pincode = x.pincode,
                dateOfBirth = x.dateOfBirth,
            }).ToListAsync();
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<TopicDetailsModel>>> GetEcom_Topic(int brandid, int pageindex)
        {
            var lstCategory = new List<EcomTopicCategory>();
            var lstProduct = new List<EcomTopicProduct>();

            var catresult = _context.Ecom_Topics.Where(x => x.isActive == true).Take(pageindex);
            foreach (var models in catresult)
            {
                if (models.Topic_CategoryYN == true)
                {
                    var subcatlist = _context.Ecom_TopicDetails_Categories.Where(x => x.FK_Topic_ID == models.Topic_ID && x.isActive == true).FirstOrDefault();
                    if (subcatlist != null)
                    {
                        lstCategory.Add(new EcomTopicCategory
                        {
                            FK_SubCategory = subcatlist.FK_SubCategory,
                            TopicDetails_FrontYN = subcatlist.TopicDetails_FrontYN,

                        });
                    }
                }
                else
                {
                    var subprodlist = _context.Ecom_TopicDetails_Products.Include(y => y.FK_Product).Where(x => x.FK_Topic_ID == models.Topic_ID && x.isActive == true).FirstOrDefault();
                    if (subprodlist != null)
                    {
                        lstProduct.Add(new EcomTopicProduct
                        {
                            FK_Productid = subprodlist.FK_Productid,
                            TopicDetails_FrontYN = subprodlist.TopicDetails_FrontYN,
                            name = subprodlist.FK_Product.Product_Name,
                        });
                    }
                }
            }

            return await _context.Ecom_Topics.Take(pageindex).Select(x => new TopicDetailsModel
            {
                Topic_ID = x.Topic_ID,
                Topic_Name = x.Topic_Name,
                ltsSubCategory = lstCategory,
                lstProduct = lstProduct
            }).ToListAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ApiResponse<IEnumerable<EcommerceTopicsModel>>>> EcomTopic()
        {
            try
            {
                var query = (from s in _context.Ecom_Topics
                             where s.isActive == true
                             select new EcommerceTopicsModel
                             {
                                 Topic_Name = s.Topic_Name,
                                 Topic_Description = s.Topic_Description,
                                 Brand_Image = s.Brand_Image,
                                 ADDYN = s.ADDYN,
                                 Slider = s.Slider,
                                 Topic_Id = s.Topic_ID,
                                 Topic_CategoryYN = s.Topic_CategoryYN,
                                 Names = s.Topic_CategoryYN == false ? _context.Ecom_TopicDetails_Products.Where(w => w.FK_Topic_ID == s.Topic_ID && w.isActive == true)
                             .Select(r => new EcommerceCategoryModel
                             {
                                 ID = r.TopicDetails_Product_ID,
                                 FK_Id = r.FK_Productid,
                                 YN = r.TopicDetails_FrontYN,
                                 Name = _context.Products.Where(w => w.productID == r.FK_Productid).Select(q => q.Product_Name).FirstOrDefault(),
                                 Path = _context.Products.Where(t => t.productID == r.FK_Productid).Select(q => q.Photo_Path).FirstOrDefault(),

                             }).ToList() :
                             _context.Ecom_TopicDetails_Categories.Where(w => w.FK_Topic_ID == s.Topic_ID && s.isActive == true).Select(r => new EcommerceCategoryModel
                             {
                                 ID = r.TopicDetails_Category_ID,
                                 FK_Id = r.FK_SubCategory,
                                 YN = r.TopicDetails_FrontYN,

                             }).ToList(),
                             }).ToList();
                List<EcommerceTopicsModel> ecommerceTopicsModels = new List<EcommerceTopicsModel>();
                if (query != null)
                {
                    ecommerceTopicsModels.AddRange(query);
                }
                return new ApiResponse<IEnumerable<EcommerceTopicsModel>> { code = 1, message = "Dashboard Contents", data = ecommerceTopicsModels };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<EcommerceTopicsModel>> { code = 1, message = ex.Message, data = null };
            }
        }

    }
}
