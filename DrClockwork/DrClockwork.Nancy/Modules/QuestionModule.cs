using System.Linq;
using DrClockwork.Domain.Models;
using DrClockwork.Nancy.ViewModels;
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
                // RavenDB
                var questions = documentSession.Query<Question>().OrderByDescending(x => x.DateAsked).ToList();

                var viewModel = new IndexViewModel
                    {
                        Questions = questions.Select(x => new QuestionViewModel(x)).ToList(),
                        Count = documentSession.Query<Question>().Count(),
                    };
                
                return View["Index", viewModel];
            };
        }
    }
}