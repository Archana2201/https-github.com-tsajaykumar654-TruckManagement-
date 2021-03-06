using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Truck.Models;
using Truck.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Truck.Entity;

namespace Truck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "v2")]
    public class AuthenticationController : ControllerBase
    {
        private readonly TruckContext _context;
        private readonly IRepos _repos;
        private readonly IConfiguration Configuration;
        private readonly iNotifier _notify;

        public AuthenticationController(TruckContext context, IConfiguration configuration, iNotifier notify, IRepos repos)
        {
            _context = context;
            Configuration = configuration;
            _notify = notify;
            _repos = repos;
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<int>>> RegisterMobile(AppUserRegistration registration)
        {
            try
            {
                Random rand = new Random();
                ConfirmationRequest request = new ConfirmationRequest
                {
                    confirmationID = 0,
                    confirmType = 4,
                    resetTime = DateTime.Now,
                    code = rand.Next(10000, 99999).ToString()
                };
                await _context.ConfirmationRequests.AddAsync(request);
                await _context.SaveChangesAsync();

                var user = new AppUser
                {
                    createdDate = DateTime.Now,
                    mobile = registration.mobile,
                    isActive = 1,
                    IsDeleted = 0,
                    verified = 0,
                    OTP = request.code,
                    OTPExpireTime = DateTime.Now,
                };
                await _context.AppUsers.AddAsync(user);
                await _context.SaveChangesAsync();


                if (registration.mobile.Contains("+91"))
                {
                    //Send SMS for indian customers
                    var Message = $"{request.code} one time password to proceed on Truck account activation.It is valid till 5 mins.Do not share your OTP with anyone.";
                    //await _repos.SendSMS(registration.mobile, Message);
                    return new ApiResponse<int> { code = 1, message = "12345" };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<int> { code = 0, message = "Mobile Number Already Exist" };
            }

            return new ApiResponse<int> { code = 1, message = "Register Successfully" };
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<ApiResponse<int>> VerifyOTP(string mobile, string token)
        {
            try
            {
                if (string.IsNullOrEmpty(mobile))
                    return new ApiResponse<int> { code = 0, message = "Invalid user" };
                if (string.IsNullOrEmpty(token))
                    return new ApiResponse<int> { code = 0, message = "Invalid token" };

                AppUser user = await _context.AppUsers.Where(x => x.mobile == mobile && x.verified == 0).FirstOrDefaultAsync();
                var appuser = await _context.ConfirmationRequests.Where(x => x.code == token && x.confirmType == 4).FirstOrDefaultAsync();
                if (appuser != null)
                {
                    var otpdate = Convert.ToDateTime(appuser.resetTime);
                    TimeSpan span = DateTime.Now.Subtract(otpdate);
                    if (span.TotalMinutes <= 5)
                    {
                        user.verified = 1;
                        _context.Update(user);
                        return new ApiResponse<int> { code = 1 ,message="Verified"};
                    }
                    return new ApiResponse<int> { code = 0, message = "OTP Expired. Please proceed to resend OTP" };
                }
                return new ApiResponse<int> { code = 0, message = "Invalid user" };
            }
            catch (Exception)
            {
                return new ApiResponse<int> { code = 0, message = "Error occured while verifying" };
            }
        }

        [HttpGet("[action]")]
        public async Task<ApiResponse<int>> ResendOTP(string mobile)
        {
            try
            {
                if (string.IsNullOrEmpty(mobile))
                    return new ApiResponse<int> { code = 0, message = "Invalid user" };

                var user = await _context.AppUsers.Where(x => x.mobile == mobile).FirstOrDefaultAsync();
                if (user != null)
                {
                    var ResendCount = await _context.ConfirmationRequests.Where(x => x.confirmType == 4).CountAsync();
                    if (ResendCount > 4)
                        return new ApiResponse<int> { code = 0, message = "Maximum SMS limit reached" };

                    Random rand = new Random();
                    ConfirmationRequest request = new ConfirmationRequest
                    {
                        confirmationID = 0,
                        confirmType = 4,
                        resetTime = DateTime.Now,
                        targetID = user.userID,
                        code = rand.Next(10000, 99999).ToString()
                    };
                    await _context.ConfirmationRequests.AddAsync(request);
                    await _context.SaveChangesAsync();

                    //Send SMS
                    if (user.mobile.Contains("+91"))
                    {
                        //Send SMS for indian customers
                        var Message = $"{request.code} one time password to proceed on Truck account activation.It is valid till 5 mins.Do not share your OTP with anyone.";
                        await _repos.SendSMS(user.mobile, Message);
                    }
                    return new ApiResponse<int> { code = 1, message = "Success" };
                }
                return new ApiResponse<int> { code = 0, message = "Invalid user" };
            }
            catch (Exception)
            {
                return new ApiResponse<int> { code = 0, message = "Error occured while Resend OTP" };
            }
        }


        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<ApiResponse<int>> CreatePin(string mobile, string pin)
        {
            AppUser user = await _context.AppUsers.Where(x => x.mobile == mobile).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Pin = pin;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return new ApiResponse<int> { code = 1 };
            }
            return new ApiResponse<int> { code = 0 };
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<AuthData>>> UserLogin(LoginModel model)
        {
            var user = await _context.AppUsers.Where(x => x.Pin == model.pin).FirstOrDefaultAsync();
            if (user != null)
            {
                SymmetricSecurityKey symmentricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtOptions:SecurityKey")));
                SigningCredentials signingCredentials = new SigningCredentials(symmentricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
                List<Claim> claims = new List<Claim>
                {
                    new Claim("mobile", user.mobile),
                    //new Claim("UserID", Adminuser.userid.ToString()),
                    //new Claim(ClaimTypes.Name, Adminuser.fullName),
                    //new Claim(ClaimTypes.Role,"Admin")
                };

                JwtSecurityToken token = new JwtSecurityToken(
                       issuer: Configuration.GetValue<string>("JwtOptions:Issuer"),
                       audience: Configuration.GetValue<string>("JwtOptions:Audience"),
                       expires: DateTime.Now.AddDays(1),
                       signingCredentials: signingCredentials,
                       claims: claims
                   );
                AuthData data = new AuthData
                {
                    accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    mobile = user.mobile,
                    //imagePath = contact.user.photoPath,
                    //name = contact.user.fullName,
                    //roles = contact.user.IUserRoles.Select(x => x.role).ToList()
                };


                return new ApiResponse<AuthData> { code = 1, message = "success", data = data };

            }

            return new ApiResponse<AuthData> { code = 0, message = "Invalid Pin" };
        }

    }
}
