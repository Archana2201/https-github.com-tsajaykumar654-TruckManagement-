using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Truck.Models;



namespace Truck.Repository
{
    public class Notifier:iNotifier
    {
        private readonly IConfiguration Configuration;
       private TruckContext _context;
        private IHttpClientFactory _clientFactory;

        public Notifier(IConfiguration _config, IHttpClientFactory clientFactory)
        {
            Configuration = _config;
            _clientFactory = clientFactory;
        }
        public async Task NotifyOnJoinRequest(List<NotificationRequest> requests, int AppType = 0)
        {
            //using (_context = new TruckContext(Configuration.GetConnectionString("DefaultConnection")))
            //{
            //    var userIDs = requests.Select(x => x.userID).ToList();
            //    var fcms = await _context.Audit.Where(x => x.deviceID != null && x.deviceID != "" && userIDs.Contains(x.userID)).Select(x => new { x.fcmID, x.userID }).ToListAsync();
            //    foreach (var item in requests)
            //    {
            //        await FCMSendNotification(item, "Join Request", fcms.FirstOrDefault(x => x.userID == x.userID).fcmID, AppType);
            //    }
            //}
        }

        public async Task NotifyOnJoinRequest(NotificationRequest request, string title, int AppType = 0)
        {
            //using (_context = new TruckContext(Configuration.GetConnectionString("DefaultConnection")))
            //{
            //    await FCMSendNotification(request, title, await _context.Audit.Where(x => x.deviceID != null && x.deviceID != "" && x.userID == request.userID).Select(x => x.fcmID).FirstOrDefaultAsync(), AppType);
            //}
        }

        private async Task FCMSendNotification(NotificationRequest notification, string title, string To, int AppType = 0)
        {
            var client = _clientFactory.CreateClient();
            var payload = new
            {
                notification = new
                {
                    //title = "Join Request",
                    title = title,
                    body = notification.message,
                },
                to = To,
                priority = "high",
                content_available = true,
                data = notification
            };
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={Configuration.GetValue<string>("FCM:Key")}");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={Configuration.GetValue<string>("FCM:SenderID")}");
            try
            {
                var result = await client.PostAsJsonAsync(Configuration.GetValue<string>("FCM:Link"), payload);
                //var result = await client.PostAsJsonAsync("http://promena.in/api/push.php", payload);
            }
            catch (Exception e)
            {
            }
        }

    }
}
