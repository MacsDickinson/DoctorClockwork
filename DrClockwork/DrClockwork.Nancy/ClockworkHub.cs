using Microsoft.AspNet.SignalR;

namespace DrClockwork.Nancy
{
    public class ClockworkHub : Hub
    {
        public void Send(string question, string answer)
        {
            Clients.All.broadcastAnswer(question, answer);
        }
    }
}