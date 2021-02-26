using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Truck.Repository
{

    public class Repos : IRepos
    {
        private IConfiguration Configuration;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IWebHostEnvironment _environment;

        public Repos(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _httpContext = httpContextAccessor;
            _environment = environment;
        }

        public string mobile => ((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("mobile").Value;
        

        public string Encrypt(string stringToEncrypt)
        {
            return stringToEncrypt;
        }

        public string Decrypt(string stringToDecrypt)
        {
            return stringToDecrypt;
        }
        public async Task<bool> SendVerifyEmail(string email, string url)
        {
            string template = await ReadTemplate("VerifyEmail.html");
            string content = template.Replace("{crUrl}", url);
            return await SendMail("Truck - Verify your Account", content, email);
        }

        public async Task<bool> SendDownloadAppEmail(string email, string url)
        {
            try
            {
                string template = await ReadTemplate("AppDownload.html");
                string content = template.Replace("{crUrl}", url);
                return await SendMail("Truck - Download App", content, email);
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        private async Task<string> ReadTemplate(string templateName)
        {
            //string pathToFile = $"{_environment.ContentRootPath}{Path.DirectorySeparatorChar}Template{Path.DirectorySeparatorChar}{templateName}";
            string pathToFile = $"{_environment.ContentRootPath}{Path.DirectorySeparatorChar}Template{Path.DirectorySeparatorChar}{templateName}";
            string builder = "";
            using (StreamReader reader = System.IO.File.OpenText(pathToFile))
            {
                builder = await reader.ReadToEndAsync();
            }
            return builder;
        }

        public async Task<bool> SendMail(string Subject, string Body, string To = null, List<string> attachments = null)
        {
            To = To ?? Configuration.GetValue<string>("SmtpMail:From");
            string UserID = Configuration.GetValue<string>("SmtpMail:UserID");
            string Password = Configuration.GetValue<string>("SmtpMail:Password");
            string SMTPPort = Configuration.GetValue<string>("SmtpMail:SMTPPort");
            string Host = Configuration.GetValue<string>("SmtpMail:Host");
            using (var client = new SmtpClient(Host))
            {
                using (var mailMessage = new MailMessage())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = UserID,
                        Password = Password
                    };

                    client.Credentials = credential;
                    client.EnableSsl = false;
                    client.Host = Host;
                    mailMessage.From = new MailAddress(Configuration.GetValue<string>("SmtpMail:From"));
                    mailMessage.To.Insert(0, new MailAddress(To));
                    mailMessage.Subject = Subject;
                    mailMessage.Body = Body;
                    mailMessage.IsBodyHtml = true;
                    await client.SendMailAsync(mailMessage);
                }
            }
            return true;
        }

        public async Task<bool> SendSMS(string mobileNo, string message)
        {
            var Result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://sms.itbizcon.com/sms/sendsms.jsp");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method  
                HttpResponseMessage response = await client.GetAsync($"?apikey={Configuration.GetValue<string>("SMS:APIKey")}&sms={message}&mobiles={mobileNo}");
                if (response.IsSuccessStatusCode)
                    //var retval = await response.Content.ReadAsAsync<string>();
                    Result = true;
                else
                    Result = false;
            }
            return Result;
        }
    }
}
