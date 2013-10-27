using Microsoft.AspNet.SignalR;

namespace DrClockwork.Web
{
    public class ClockworkHub : Hub
    {
        public void Send(string id)
        {
            Clients.All.broadcastEventId(id);
        }
    }
}