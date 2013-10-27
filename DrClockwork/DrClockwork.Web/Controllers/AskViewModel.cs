namespace DrClockwork.Web.Controllers
{
    public class AskViewModel
    {
        public AskViewModel()
        {
            SMS = new SMS();
        }
        public SMS SMS { get; set; }
        public string ID { get; set; }
        public bool Success { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Answer { get; set; }
    }
}