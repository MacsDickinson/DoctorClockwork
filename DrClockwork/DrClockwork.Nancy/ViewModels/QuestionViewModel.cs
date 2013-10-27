using DrClockwork.Domain.Models;

namespace DrClockwork.Nancy.ViewModels
{
    public class QuestionViewModel
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Content { get; set; }
        public string Msg_Id { get; set; }
        public string DateAsked { get; set; }
        public string Keyword { get; set; }
        public string Answer { get; set; }

        public QuestionViewModel(Question question)
        {
            To = question.ToPhoneNumber;
            From = question.FromPhoneNumber;
            Content = question.Content;
            Msg_Id = question.MessageId;
            DateAsked = question.DateAsked.ToString("g");
            Keyword = question.Keyword;
            Answer = question.Answer;
        }
    }
}