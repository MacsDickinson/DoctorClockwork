using System.Collections.Generic;

namespace DrClockwork.Nancy.ViewModels
{
    public class IndexViewModel
    {
        public List<QuestionViewModel> Questions { get; set; }
        public string path { get; set; }
    }
}