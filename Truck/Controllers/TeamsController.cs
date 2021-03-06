using System;
using System.Collections.Generic;
using System.Linq;
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
using Razorpay.Api;

namespace Truck.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v2")]
    [ApiController]
    public class TeamsController : ControllerBase
    {

        private readonly TruckContext _context;
        private readonly IRepos _repos;
        private readonly IStorage _storage;
        public TeamsController(TruckContext context, IRepos repos, IStorage storage)
        {
            _context = context;
            _repos = repos;
            _storage = storage;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<TeamModel>>> CreateOrUpdateTeams(TeamModel model)
        {
            try
            {
                var editteam = await _context.Teams.Where(x => x.Team_ID == model.Team_ID).FirstOrDefaultAsync();
                Team team = (editteam == null) ? new Team() : editteam;

                model.Name = model.Name.TrimStart().TrimEnd();
                team.Name = (string.IsNullOrEmpty(model.Name)) ? team.Name : model.Name;
                team.Mobile = (string.IsNullOrEmpty(model.Mobile)) ? team.Mobile : model.Mobile;
                team.Branch = model.Branch;
                team.Roles = model.Roles;

                if (editteam == null)
                {
                    _context.Teams.Add(team);
                }

                await _context.SaveChangesAsync();

                return new ApiResponse<TeamModel> { code = 1, message = "Successfull" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<TeamModel> { code = 0, message = ex.Message, data = null };
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ApiResponse<List<Select>>>> TeamRolesList()
        {
            var list = await _context.Teams_Roles.Select(x => new Select
            {
                name = x.Team_RoleType,
                value = x.Team_RoleID
            }).ToListAsync();
            return new ApiResponse<List<Select>> { code = 1, data = list };
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<TeamModel>>> TeamList()
        {
            return await _context.Teams.Select(x => new TeamModel
            {
                Name = x.Name,
                Mobile = x.Mobile,
                Branch = x.Branch,
                //Roles= _context.Teams_Roles.Where(w => w.Team_RoleID == x.Team_ID).Select(s => s.Team_RoleType).FirstOrDefaultAsync()
            }).ToListAsync();
        }


       

    }
}
