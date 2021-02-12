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
            //appUser.company = form.company;
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
            catch
            {
                return new ApiResponse<AppUser> { code = 0, message = "" };
            }
            return new ApiResponse<AppUser> { code = 1, message = "" };
        }

        //[HttpPost("[action]")]
        //public async Task<ActionResult<ApiResponse<VehiclesModel>>> CreateOrUpdateVehicles(VehiclesModel model)
        //{
        //    try
        //    {
        //        var vehicles = await _context.veh.Where(x => x.BrandID == model.brandid).FirstOrDefaultAsync();

        //        Ecom_Brand_Appsettings Settings = (Appsettings == null) ? new Ecom_Brand_Appsettings() : Appsettings;

        //        if (model.imageFile != null)
        //        {
        //            if (model.imageFile.FileName.Length > 0)
        //            {
        //                if (!model.extension.Contains("."))
        //                    model.extension = "." + model.extension;

        //                string FullPath = await _storage.Save(model.imageFile, "/EcomApp", model.extension);
        //                //save image else leave
        //                Settings.AppLogo = FullPath;
        //            }
        //        }

        //        model.appName = model.appName.TrimStart().TrimEnd();

        //        Settings.AppName = (string.IsNullOrEmpty(model.appName)) ? Settings.AppName : model.appName;
        //        Settings.BrandID = (model.brandid == 0) ? Settings.BrandID : model.brandid;

        //        Settings.PrimaryColor = (string.IsNullOrEmpty(model.primaryColor)) ? Settings.PrimaryColor : model.primaryColor;
        //        Settings.CardColor = (string.IsNullOrEmpty(model.cardColor)) ? Settings.CardColor : model.cardColor;
        //        Settings.fontFamily = (string.IsNullOrEmpty(model.fontFamily)) ? Settings.fontFamily : model.fontFamily;
        //        Settings.FontSize = (model.fontSize == 0) ? Settings.FontSize : model.fontSize;
        //        Settings.isDarkTheme = model.isDarkTheme;
        //        Settings.HintHightlightColor = (string.IsNullOrEmpty(model.hintHightlightColor)) ? Settings.HintHightlightColor : model.hintHightlightColor;
        //        Settings.buttonColor = (string.IsNullOrEmpty(model.buttonColor)) ? Settings.buttonColor : model.buttonColor;
        //        Settings.Razorkey = (string.IsNullOrEmpty(model.razorkey)) ? Settings.Razorkey : model.razorkey;
        //        Settings.RazorPaySecretAccesskey = (string.IsNullOrEmpty(model.razorPaySecretAccesskey)) ? Settings.RazorPaySecretAccesskey : model.razorPaySecretAccesskey;
        //        Settings.Shipping_Charge = (model.Shipping_Charge == 0) ? Settings.Shipping_Charge : model.Shipping_Charge;
        //        if (Appsettings == null)
        //        {
        //            _context.Ecom_Brand_Appsettings.Add(Settings);
        //        }

        //        await _context.SaveChangesAsync();

        //        return new ApiResponse<EcomSettingsModel> { code = 1, message = "Successfull" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResponse<EcomSettingsModel> { code = 0, message = ex.Message, data = null };
        //    }
        //}

    }
}
