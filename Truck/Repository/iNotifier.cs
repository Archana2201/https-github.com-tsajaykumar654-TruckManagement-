using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truck.Models;

namespace Truck.Repository
{
    public interface iNotifier
    {

        Task NotifyOnJoinRequest(NotificationRequest request, string title, int AppType = 0);
        Task NotifyOnJoinRequest(List<NotificationRequest> requests, int AppType = 0);
    }
}
