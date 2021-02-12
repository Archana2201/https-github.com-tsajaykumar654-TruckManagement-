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
        public MobileController(TruckContext context, IRepos repos)
        {
            _context = context;
            _repos = repos;

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
    }
}
