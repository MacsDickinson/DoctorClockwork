using System;

namespace DrClockwork.Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string ToPhoneNumber { get; set; }
        public string FromPhoneNumber { get; set; }
        public DateTime DateAsked { get; set; }
        public string Content { get; set; }
        public string MessageId { get; set; }
        public string Keyword { get; set; }
        public string Answer { get; set; }
    }
}
