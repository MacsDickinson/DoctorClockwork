using System.IO;
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
        public QuestionModule(IDocumentSession documentSession)
        {
            Get["/"] = _ =>
            {
                var questions = documentSession.Query<Question>().ToList();

                var model = new IndexViewModel
                {
                    Questions = questions.Select(x => new QuestionViewModel(x)).ToList(),
                    path = System.Web.HttpContext.Current.Server.MapPath(@"~/config"),
                    FileExists = File.Exists(@"C:\DWASFiles\Sites\drclockwork\VirtualDirectory0\site\wwwroot\config\Settings.xml"),
                    DirectoryExists = Directory.Exists(@"C:\DWASFiles\Sites\drclockwork\VirtualDirectory0\site\wwwroot\config\Settings.xml")
                };
                model.Files = model.DirectoryExists
                    ? string.Join(", ",
                        Directory.GetFiles(@"C:\DWASFiles\Sites\drclockwork\VirtualDirectory0\site\wwwroot\config"))
                    : "";
                return View["Index", model];
            };
        }
    }
}