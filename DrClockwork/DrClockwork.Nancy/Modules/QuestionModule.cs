using System.Linq;
using DrClockwork.Domain.Models;
using DrClockwork.Nancy.ViewModels;
using Microsoft.AspNet.SignalR;
using Nancy;
using Raven.Client;
using Raven.Client.Linq;

namespace DrClockwork.Nancy.Modules
{
    public class QuestionModule : NancyModule
    {
        public QuestionModule(IDocumentSession documentSession, IHubContext hubContext)
        {
            Get["/"] = _ =>
            {
                var model = new IndexViewModel
                {
                    Questions = documentSession.Query<Question>().Select(x => new QuestionViewModel(x)).ToList()
                };

                return View["Index", model];
            };
        }
    }
}