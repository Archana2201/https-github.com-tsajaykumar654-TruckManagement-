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
        public async Task<ActionResult<ApiResponse<AppUser>>> UpdateUserProfile(AppUserForm form)
        {
            if (string.IsNullOrEmpty(form.email) || string.IsNullOrEmpty(form.fullName) || string.IsNullOrEmpty(form.mobile))
            {
                return new ApiResponse<AppUser> { code = 0, message = "Null Values" };
            }
            var appUser = await _context.AppUsers.FindAsync(_repos.mobile);
            if (!string.IsNullOrEmpty(form.profileImage))
            {
                if (appUser.dpPath != "/images/profile-picture/default.png")
                {
                    await _storage.DeleteIfExists(appUser.dpPath);
                }
                appUser.dpPath = await _storage.Save(form.profileImage, "/profile-picture");
            }
            if (!string.IsNullOrEmpty(form.aadharcard))
            {
                if (appUser.AadharCard != "/images/profile-picture/default.png")
                {
                    await _storage.DeleteIfExists(appUser.AadharCard);
                }
                appUser.AadharCard = await _storage.Save(form.aadharcard, "/profile-picture");
            }
            if (!string.IsNullOrEmpty(form.pancard))
            {
                if (appUser.PanCard != "/images/profile-picture/default.png")
                {
                    await _storage.DeleteIfExists(appUser.PanCard);
                }
                appUser.PanCard = await _storage.Save(form.pancard, "/profile-picture");
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
                return new ApiResponse<AppUser> { code = 0, message = "Error" };
            }
            return new ApiResponse<AppUser> { code = 1, message = "Success" };
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
        public async Task<ActionResult<ApiResponse<VehicleInfoModel>>> CreateOrUpdateVehicle(VehicleInfoModel model)
        {
            try
            {
                var vehicle = await _context.Vehicle_Renewal_Infos.Where(x => x.VehicleRenewalInfo_ID == model.VehicleRenewalInfo_ID).FirstOrDefaultAsync();
                Vehicle_Renewal_Info vehicleinfo = (vehicle == null) ? new Vehicle_Renewal_Info() : vehicle;

                model.Vehicle_Number = model.Vehicle_Number.TrimStart().TrimEnd();

                vehicleinfo.Vehicle_Model_ID = (model.Vehicle_Model_ID == 0) ? vehicleinfo.Vehicle_Model_ID : model.Vehicle_Model_ID;
                vehicleinfo.Vehicle_Number = (string.IsNullOrEmpty(model.Vehicle_Number)) ? vehicleinfo.Vehicle_Number : model.Vehicle_Number;
                vehicleinfo.VehicleRenewalInfo_ID = (model.VehicleRenewalInfo_ID == 0) ? vehicleinfo.VehicleRenewalInfo_ID : model.VehicleRenewalInfo_ID;
                vehicleinfo.Vehicle_ModelNumber = (string.IsNullOrEmpty(model.Vehicle_ModelNumber)) ? vehicleinfo.Vehicle_ModelNumber : model.Vehicle_ModelNumber;
               // vehicleinfo.Vehicle_Company_ID = (model.Vehicle_Company_ID == 0) ? vehicleinfo.Vehicle_Company_ID : model.Vehicle_Company_ID;
                vehicleinfo.vehicle_type = model.vehicle_type;//0--Personal n 1--Corporate
                if (vehicle == null)
                {
                    _context.Vehicle_Renewal_Infos.Add(vehicleinfo);
                }

                await _context.SaveChangesAsync();

                return new ApiResponse<VehicleInfoModel> { code = 1, message = "Successfull" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<VehicleInfoModel> { code = 0, message = ex.Message, data = null };
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<VehicleDocumentModel>>> UploadDocuments(VehicleDocumentModel model)
        {
            try
            {
                var vehicle = await _context.Vehicle_Documents.Where(x => x.VehicleDocuments_ID == model.VehicleDocuments_ID).FirstOrDefaultAsync();
                Vehicle_Document vehicledocs = (vehicle == null) ? new Vehicle_Document() : vehicle;

                if (!string.IsNullOrEmpty(model.Vehicle_FrontImage))
                {
                    if (vehicledocs.Vehicle_FrontImage != "/images/profile-picture/default.png")
                    {
                        await _storage.DeleteIfExists(vehicledocs.Vehicle_FrontImage);
                    }
                    vehicledocs.Vehicle_FrontImage = await _storage.Save(model.Vehicle_FrontImage, "/Vehicle-Docs");
                }
                if (!string.IsNullOrEmpty(model.Vehicle_BackImage))
                {
                    if (vehicledocs.Vehicle_BackImage != "/images/profile-picture/default.png")
                    {
                        await _storage.DeleteIfExists(vehicledocs.Vehicle_BackImage);
                    }
                    vehicledocs.Vehicle_BackImage = await _storage.Save(model.Vehicle_BackImage, "/Vehicle-Docs");
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

                return new ApiResponse<VehicleDocumentModel> { code = 1, message = "Successfull" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<VehicleDocumentModel> { code = 0, message = ex.Message, data = null };
            }
        }



    }
}
