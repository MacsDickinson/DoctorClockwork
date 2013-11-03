using System.Linq;
using DrClockwork.Domain.Models;
using DrClockwork.Nancy.ViewModels;
using Nancy;
using Raven.Client;
using Raven.Client.Linq;
using TweetSharp;

namespace DrClockwork.Nancy.Modules
{
    public class QuestionModule : NancyModule
    {
        private const string consumerKey = "u67Ns1KSSYZWiGh1jJrPxw";
        private const string consumerSecret = "MhwBuBOyF1KSU9EICA3no79axmbn8kfBPPCZDyTJU";
        private const string accessToken = "2161079472-XvChIme92Yxhx7cIr43WAuAvazKHvsVFzmhae6k";
        private const string accessTokenSecret = "QZ1c0uO3161aoD9GWqOwcBYJUU2tj1EGnU10iyEoBHXzB";

        public QuestionModule(IDocumentSession documentSession)
        {
            Get["/"] = _ =>
            {
                // Twitter
                var twitterService = new TwitterService(consumerKey, consumerSecret);
                twitterService.AuthenticateWith(accessToken, accessTokenSecret);
                var tweets = twitterService.ListTweetsMentioningMe(new ListTweetsMentioningMeOptions());

                var twitterQuestions = tweets.Select(tweet => new QuestionViewModel
                {
                    Answer = "TODO", Channel = MessageChannel.Twitter, Content = tweet.Text, DateAsked = tweet.CreatedDate, From = tweet.User.ScreenName
                }).ToList();

                // RavenDB
                var questions = documentSession.Query<Question>().OrderByDescending(x => x.DateAsked).ToList();

                var viewModel = new IndexViewModel
                    {
                        Questions = questions.Select(x => new QuestionViewModel(x)).ToList(),
                        Count = documentSession.Query<Question>().Count(),
                    };
                viewModel.Questions.AddRange(twitterQuestions);
                return View["Index", viewModel];
            };
        }
    }
}