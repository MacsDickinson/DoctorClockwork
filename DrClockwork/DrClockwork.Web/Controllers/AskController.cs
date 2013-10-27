using System;
using System.Net;
using System.Web.Mvc;
using AIMLbot;
using Clockwork;
using DrClockwork.Web.Models;

namespace DrClockwork.Web.Controllers
{
    public class AskController : Controller
    {
        //
        // GET: /Ask/

        public ActionResult Index(AskViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.To))
            {
                viewModel.From = "447967172773";
            }

            if (string.IsNullOrEmpty(viewModel.Content))
            {
                viewModel.Content = "I am hungry";
            }

            var pathToAiml = System.Web.HttpContext.Current.Server.MapPath(@"~/aiml");
            var clockworkBot = new Bot();
            //var configFile = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/config"), "Settings.xml");
            clockworkBot.loadSettings(@"C:\config\Settings.xml");
            var loader = new AIMLbot.Utils.AIMLLoader(clockworkBot);
            loader.loadAIML(pathToAiml);
            clockworkBot.isAcceptingUserInput = false;
            clockworkBot.isAcceptingUserInput = true;

            var patient = new User(viewModel.From, clockworkBot);
            var request = new Request(viewModel.Content, patient, clockworkBot);
            var answer = clockworkBot.Chat(request);
            viewModel.Answer = answer.Output;

            try
            {
                var api = new API("4832b0bded15eb58c54bb7d3cf01a08029bb41d8");
                var sms = new SMS { To = viewModel.From, Message = answer.Output };
                var result = api.Send(sms);

                if (result.Success)
                {
                    //Console.WriteLine("SMS Sent to {0}, Clockwork ID: {1}", result.SMS.To, result.ID);
                }
                else
                {
                    //Console.WriteLine("SMS to {0} failed, Clockwork Error: {1} {2}", result.SMS.To, result.ErrorCode, result.ErrorMessage);
                }
            }
            catch (APIException ex)
            {
                // You'll get an API exception for errors
                // such as wrong username or password
                //Console.WriteLine("API Exception: " + ex.Message);
            }
            catch (WebException ex)
            {
                // Web exceptions mean you couldn't reach the Clockwork server
                //Console.WriteLine("Web Exception: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                // Argument exceptions are thrown for missing parameters,
                // such as forgetting to set the username
                //Console.WriteLine("Argument Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Something else went wrong, the error message should help
                //Console.WriteLine("Unknown Exception: " + ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
