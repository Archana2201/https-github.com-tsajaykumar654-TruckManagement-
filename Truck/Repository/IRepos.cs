using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Truck.Repository
{
    public interface IRepos
    {
        int UserID { get; }
       

        string mobile { get; }
        string Encrypt(string stringToEncrypt);
        string Decrypt(string stringToDecrypt);
        Task<bool> SendVerifyEmail(string email, string To = null);
       
        Task<bool> SendMail(string Subject, string Body, string To = null, List<string> attachments = null);
        Task<bool> SendDownloadAppEmail(string email, string url);
        Task<bool> SendSMS(string mobileNo, string message);
    }
}
