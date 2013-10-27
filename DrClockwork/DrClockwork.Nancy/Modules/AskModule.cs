using System;
using DrClockwork.Domain.Logic;
using DrClockwork.Domain.Models;
using DrClockwork.Nancy.ViewModels;
using Microsoft.AspNet.SignalR;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;
using Raven.Client;

namespace DrClockwork.Nancy.Modules
{
    public class AskModule : NancyModule
    {
        public AskModule(IDocumentSession documentSession, IHubContext hubContext)
            : base("Ask")
        {
            Get["/"] = _ =>
            {
                var model = this.Bind<AskViewModel>();
                
                var pathToAiml = System.Web.HttpContext.Current.Server.MapPath(@"~/aiml");

                var drClockwork = new DoctorClockwork(pathToAiml);

                var answer = drClockwork.AskMeAnything(model.From, model.Content);
                    
                ClockworkSMS.Send(model.From, answer);

                var question = new Question
                {
                    ToPhoneNumber = model.To,
                    FromPhoneNumber = model.From,
                    DateAsked = DateTime.Now,
                    Content = model.Content,
                    MessageId = model.Msg_Id,
                    Keyword = model.Keyword,
                    Answer = answer
                };

                documentSession.Store(question);
                documentSession.SaveChanges();

                hubContext.Clients.All.broadcastAnswer(model.Content, answer);

                return null;
            };
        }
    }
}