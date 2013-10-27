using DrClockwork.Domain.Models;

namespace DrClockwork.Nancy.ViewModels
{
    public class QuestionViewModel
    {
        private string _from;

        public string To { get; set; }

        public string From
        {
            get
            {
                return string.Format("{0}*****{1}", _from.Substring(0, 2), _from.Substring(7, _from.Length - 7));
            }
            set
            {
                _from = value;
            }
        }

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