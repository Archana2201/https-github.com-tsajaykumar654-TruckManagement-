using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
using Truck.Entity;

namespace Truck.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]

    public class AdminController : ControllerBase
    {
        private readonly TruckContext _context;
        private readonly IRepos _repos;
        private readonly IStorage _storage;
        public AdminController(TruckContext context, IRepos repos, IStorage storage)
        {
            _context = context;
            _repos = repos;
            _storage = storage;

        }


        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<LanguageMasterModel>>> CreateLanguageMaster(LanguageMasterModel form)
        {
            try
            {
                if (string.IsNullOrEmpty(form.Language))
                {
                    return new ApiResponse<LanguageMasterModel> { code = 0, message = "Invalid Details" };
                }
                form.Language = form.Language.TrimStart().TrimEnd().ToUpper();
                var lang = await _context.Language_Masters.Where(x => x.Language == form.Language).FirstOrDefaultAsync();
                if (lang == null)
                {
                    lang = new Language_Master
                    {
                        Language = form.Language,
                    };
                    _context.Add(lang);
                    await _context.SaveChangesAsync();
                    var query = _context.Language_Masters.Select(s => new LanguageMasterModel
                    {
                        Language = form.Language
                    }).FirstOrDefault();

                    return new ApiResponse<LanguageMasterModel> { code = 1, message = "Success", data = query };
                }

                else
                {
                    return new ApiResponse<LanguageMasterModel> { code = 0, message = "Already Exists" };
                }
            }

            catch (Exception)
            {
                return new ApiResponse<LanguageMasterModel> { code = 0, message = "Failed" };
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<PageMasterModel>>> CreatePageMaster(PageMasterModel form)
        {
            try
            {
                if (string.IsNullOrEmpty(form.Page_Name))
                {
                    return new ApiResponse<PageMasterModel> { code = 0, message = "Invalid Details" };
                }
                form.Page_Name = form.Page_Name.TrimStart().TrimEnd().ToUpper();
                var page = await _context.Page_Masters.Where(x => x.Page_Name == form.Page_Name).FirstOrDefaultAsync();
                if (page == null)
                {
                    page = new Page_Master
                    {
                        Page_Name = form.Page_Name,
                    };
                    _context.Add(page);
                    await _context.SaveChangesAsync();
                    var query = _context.Language_Masters.Select(s => new PageMasterModel
                    {
                        Page_Name = form.Page_Name
                    }).FirstOrDefault();

                    return new ApiResponse<PageMasterModel> { code = 1, message = "Success", data = query };
                }

                else
                {
                    return new ApiResponse<PageMasterModel> { code = 0, message = "Already Exists" };
                }
            }

            catch (Exception)
            {
                return new ApiResponse<PageMasterModel> { code = 0, message = "Failed" };
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<MenuMasterModel>>> CreateMenuMaster(MenuMasterModel form)
        {
            try
            {
                if (string.IsNullOrEmpty(form.Menu_Name))
                {
                    return new ApiResponse<MenuMasterModel> { code = 0, message = "Invalid Details" };
                }
                form.Menu_Name = form.Menu_Name.TrimStart().TrimEnd().ToUpper();
                var lang = await _context.Menu_Masters.Where(x => x.Menu_Name == form.Menu_Name).FirstOrDefaultAsync();
                if (lang == null)
                {
                    lang = new Menu_Master
                    {
                        Menu_Name = form.Menu_Name,
                    };
                    _context.Add(lang);
                    await _context.SaveChangesAsync();
                    var query = _context.Language_Masters.Select(s => new MenuMasterModel
                    {
                        Menu_Name = form.Menu_Name
                    }).FirstOrDefault();

                    return new ApiResponse<MenuMasterModel> { code = 1, message = "Success", data = query };
                }

                else
                {
                    return new ApiResponse<MenuMasterModel> { code = 0, message = "Already Exists" };
                }
            }

            catch (Exception)
            {
                return new ApiResponse<MenuMasterModel> { code = 0, message = "Failed" };
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<int>>> CreateMenuLanguageMapping(LangMenuModel model)
        {
            LanguageMapping_Master lang = new LanguageMapping_Master();
            var lstlangmenu = new List<LanguageMapping_Master>();

            try
            {
                foreach (var models in model.langmap)
                {
                    lstlangmenu.Add(new LanguageMapping_Master
                    {
                        Fk_Language_ID = models.Fk_Language_ID,
                        Fk_Page_ID = models.Fk_Page_ID,
                        Fk_Menu_ID = models.Fk_Menu_ID,
                        Keys = models.Keys,
                        Descriptions = models.Descriptions,
                        Types = models.Types,
                    });
                }
                await _context.LanguageMapping_Masters.AddRangeAsync(lstlangmenu);
                await _context.SaveChangesAsync();

                return new ApiResponse<int> { code = 1, data = lang.LanguageMapping_ID, message = "Success" };

            }
            catch (Exception ex)
            {
                return new ApiResponse<int> { code = 1, message = ex.Message };
            }

        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ApiResponse<List<Select>>>> TruckLanguageMasterDDL()
        {
            var list = await _context.Language_Masters.Select(x => new Select
            {
                name = x.Language,
                value = x.Language_ID
            }).ToListAsync();
            return new ApiResponse<List<Select>> { code = 1, data = list };
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ApiResponse<List<Select>>>> TruckPageMasterDDL()
        {
            var list = await _context.Page_Masters.Select(x => new Select
            {
                name = x.Page_Name,
                value = x.Page_ID
            }).ToListAsync();
            return new ApiResponse<List<Select>> { code = 1, data = list };
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ApiResponse<List<Select>>>> TruckMenuMasterDDL()
        {
            var list = await _context.Menu_Masters.Select(x => new Select
            {
                name = x.Menu_Name,
                value = x.Menu_ID
            }).ToListAsync();
            return new ApiResponse<List<Select>> { code = 1, data = list };
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<int>>> CreateNavigation(NavigationModel form)
        {
            try
            {
                var nav = new Navigation_Master
                {
                    Fk_Language_ID = form.Fk_Language_ID,
                    Fk_User_ID = form.Fk_User_ID,
                    Keys = form.Keys,
                    Descriptions = form.Descriptions,
                };
                _context.Add(nav);
                await _context.SaveChangesAsync();
                return new ApiResponse<int> { code = 1, message = "Success" };
            }
            catch (Exception)
            {
                return new ApiResponse<int> { code = 0, message = "Failed" };
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<VehicleCompanyModel>>> CreateOrUpdateVehicleCompany(VehicleCompanyModel model)
        {
            try
            {
                var VehicleCompany = await _context.Vehicle_Company_Masters.Where(x => x.VehicleCompany_ID == model.VehicleCompany_ID).FirstOrDefaultAsync();

                Vehicle_Company_Master company = (VehicleCompany == null) ? new Vehicle_Company_Master() : VehicleCompany;
                model.VehicleCompany_Name = model.VehicleCompany_Name.TrimStart().TrimEnd();
                company.VehicleCompany_Name = (string.IsNullOrEmpty(model.VehicleCompany_Name)) ? company.VehicleCompany_Name : model.VehicleCompany_Name;
                //company.VehicleCompany_ID = (model.VehicleCompany_ID == 0) ? company.VehicleCompany_ID : model.VehicleCompany_ID;
                if (VehicleCompany == null)
                {
                    _context.Vehicle_Company_Masters.Add(company);
                }

                await _context.SaveChangesAsync();

                return new ApiResponse<VehicleCompanyModel> { code = 1, message = "Successfull" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<VehicleCompanyModel> { code = 0, message = ex.Message, data = null };
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<VehicleModel>>> CreateOrUpdateVehiclemodel(VehicleModel model)
        {
            try
            {
                var Vehiclemodel = await _context.Vehicle_Model_Masters.Where(x => x.VehicleModel_ID == model.VehicleModel_ID).FirstOrDefaultAsync();

                Vehicle_Model_Master vehiclemodels = (Vehiclemodel == null) ? new Vehicle_Model_Master() : Vehiclemodel;
                model.VehicleModel_Name = model.VehicleModel_Name.TrimStart().TrimEnd();
                vehiclemodels.VehicleModel_Name = (string.IsNullOrEmpty(model.VehicleModel_Name)) ? vehiclemodels.VehicleModel_Name : model.VehicleModel_Name;
                //vehiclemodels.VehicleModel_ID = (model.VehicleModel_ID == 0) ? vehiclemodels.VehicleModel_ID : model.VehicleModel_ID;
                if (Vehiclemodel == null)
                {
                    _context.Vehicle_Model_Masters.Add(vehiclemodels);
                }

                await _context.SaveChangesAsync();

                return new ApiResponse<VehicleModel> { code = 1, message = "Successfull" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<VehicleModel> { code = 0, message = ex.Message, data = null };
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<int>>> AddEcomTopic([FromForm] EcomTopicModel form)
        {
            try
            {
                var query = _context.Ecom_Topics.Where(w => w.Topic_ID == form.Topic_Id && w.isActive == true).Count();
                if (query == 0)
                {
                    Ecom_Topic ecomtopic = new Ecom_Topic();
                    ecomtopic.Topic_Name = form.Topic_Name;
                    ecomtopic.Topic_Description = form.Topic_Description;
                   
                    ecomtopic.ADDYN = form.ADDYN;
                    ecomtopic.Slider = form.Slider;
                    ecomtopic.Topic_CategoryYN = form.Topic_CategoryYN;
                    string FullPath = await _storage.Save(form.filename, "/product-images", form.extension);
                    ecomtopic.Brand_Image = FullPath;
                    ecomtopic.isActive = true;

                    _context.Ecom_Topics.Add(ecomtopic);
                    await _context.SaveChangesAsync();
                    return new ApiResponse<int> { code = 1, data = ecomtopic.Topic_ID, message = "Success" };

                }
                else
                {
                    var existingTopic = _context.Ecom_Topics.Where(w => w.Topic_ID == form.Topic_Id && w.isActive == true).FirstOrDefault();
                    if (form.filename != null)
                        existingTopic.Brand_Image = await _storage.Save(form.filename, "/product-images", form.extension);
                    existingTopic.Topic_CategoryYN = form.Topic_CategoryYN;
                    existingTopic.Topic_Description = form.Topic_Description;
                    existingTopic.Topic_Name = form.Topic_Name;
                    existingTopic.Slider = form.Slider;
                    existingTopic.ADDYN = form.ADDYN;
                    _context.Ecom_Topics.Update(existingTopic);
                    await _context.SaveChangesAsync();
                    return new ApiResponse<int> { code = 1, data = existingTopic.Topic_ID, message = "Success" };

                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<int> { code = 0, data = 0 };
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<int>>> EcomAddTopicProducts(EcomTopicProdDetailsModel model)
        {
            Ecom_TopicDetails_Product ecomtopicpoducts = new Ecom_TopicDetails_Product();
            var lsttopicprod = new List<Ecom_TopicDetails_Product>();

            try
            {
                foreach (var models in model.prodlst)
                {
                    lsttopicprod.Add(new Ecom_TopicDetails_Product
                    {
                        
                        FK_Productid = models.value,
                        TopicDetails_FrontYN = false,
                        FK_Topic_ID = model.FK_Topic_ID,
                        isActive = true
                    });
                }
                await _context.Ecom_TopicDetails_Products.AddRangeAsync(lsttopicprod);
                await _context.SaveChangesAsync();

                return new ApiResponse<int> { code = 1, data = ecomtopicpoducts.TopicDetails_Product_ID, message = "Success" };

            }
            catch (Exception ex)
            {
                return new ApiResponse<int> { code = 1, message = ex.Message };
            }

        }


        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<int>>> EcomAddTopicSubCategory(EcomTopicSubCatDetailsModel model)
        {
            Ecom_TopicDetails_Category ecomtopicpoducts = new Ecom_TopicDetails_Category();
            var lsttopicprod = new List<Ecom_TopicDetails_Category>();

            try
            {
                foreach (var models in model.prodlst)
                {
                    lsttopicprod.Add(new Ecom_TopicDetails_Category
                    {
                       
                        FK_SubCategory = models.value,
                        TopicDetails_FrontYN = false,
                        FK_Topic_ID = model.FK_Topic_ID,
                        isActive = true
                    });
                }
                await _context.Ecom_TopicDetails_Categories.AddRangeAsync(lsttopicprod);
                await _context.SaveChangesAsync();

                return new ApiResponse<int> { code = 1, data = ecomtopicpoducts.TopicDetails_Category_ID, message = "Success" };

            }
            catch (Exception ex)
            {
                return new ApiResponse<int> { code = 1, message = ex.Message };
            }

        }


    }
}
