using System;
using System.Linq;
using DrClockwork.Domain.Logic;
using DrClockwork.Domain.Models;
using DrClockwork.Nancy.ViewModels;
using Microsoft.AspNet.SignalR;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;
using Raven.Client;
using TweetSharp;

namespace DrClockwork.Nancy.Modules
{
    public class TweetModule : NancyModule
    {
        private const string consumerKey = "u67Ns1KSSYZWiGh1jJrPxw";
        private const string consumerSecret = "MhwBuBOyF1KSU9EICA3no79axmbn8kfBPPCZDyTJU";
        private const string accessToken = "2161079472-XvChIme92Yxhx7cIr43WAuAvazKHvsVFzmhae6k";
        private const string accessTokenSecret = "QZ1c0uO3161aoD9GWqOwcBYJUU2tj1EGnU10iyEoBHXzB";

        public TweetModule(IDocumentSession documentSession, IHubContext hubContext)
            : base("Tweet")
        {
            Post["/"] = _ =>
            {
                // Twitter
                var twitterService = new TwitterService(consumerKey, consumerSecret);
                twitterService.AuthenticateWith(accessToken, accessTokenSecret);
                var tweets = twitterService.ListTweetsMentioningMe(new ListTweetsMentioningMeOptions());

                var twitterQuestions = tweets.Select(tweet => new QuestionViewModel
                {
                    Answer = "TODO",
                    Channel = MessageChannel.Twitter,
                    Content = tweet.Text,
                    DateAsked = tweet.CreatedDate,
                    From = tweet.User.ScreenName
                }).ToList();

                var viewModel = new IndexViewModel();

                return View["Index", viewModel];
            };
        }
    }
}