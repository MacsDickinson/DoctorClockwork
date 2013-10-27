using Microsoft.AspNet.SignalR;
using Nancy;
using Raven.Client;

namespace DrClockwork.Nancy.Modules
{
    public class AskModule : NancyModule
    {
        public AskModule(IDocumentSession documentSession, IHubContext hubContext)
        {
            Get["/"] = _ =>
            {
                return View["Index"];
            };
        }
    }
}