using DrClockwork.Domain.Logic;
using DrClockwork.Nancy.ViewModels;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;

namespace DrClockwork.Nancy.Modules
{
    public class AskModule : NancyModule
    {
        public AskModule() : base("Ask")
        {
            Post["/"] = _ =>
            {
                var model = this.Bind<AskViewModel>();
                var result = this.Validate(model);

                if (result.IsValid)
                {
                    var pathToAiml = System.Web.HttpContext.Current.Server.MapPath(@"~/aiml");

                    var drClockwork = new DoctorClockwork(pathToAiml);

                    var answer = drClockwork.AskMeAnything(model.From, model.Content);

                    ClockworkSMS.Send(model.From, answer);
                } 
                return null;
            };
        }
    }
}