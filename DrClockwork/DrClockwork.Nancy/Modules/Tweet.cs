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
    public class TweetModule : NancyModule
    {
        public TweetModule(IDocumentSession documentSession, IHubContext hubContext)
            : base("Tweet")
        {
            Post["/"] = _ =>
            {
                try
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
                    var from = string.Format("{0}*****{1}", model.From.Substring(0, 2), model.From.Substring(7, model.From.Length - 7));
                    hubContext.Clients.All.broadcastAnswer(model.Content, answer, from);

                    return null;
                }
                catch (Exception ex)
                {
                    return string.Format("Message: {0}\r\nDetail {1}", ex.Message, ex.StackTrace);
                }
                
            };
        }
    }
}