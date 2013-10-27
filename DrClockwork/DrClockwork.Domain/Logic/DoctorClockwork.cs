using AIMLbot;

namespace DrClockwork.Domain.Logic
{
    public class DoctorClockwork
    {
        private readonly string _aimlPath;
        private const string SettingsPath = @"C:\config\Settings.xml";

        public DoctorClockwork(string aimlPath)
        {
            _aimlPath = aimlPath;
        }

        public string AskMeAnything(string name, string question)
        {
            var clockworkBot = new Bot();
            clockworkBot.loadSettings(SettingsPath);
            var loader = new AIMLbot.Utils.AIMLLoader(clockworkBot);
            loader.loadAIML(_aimlPath);
            clockworkBot.isAcceptingUserInput = false;
            clockworkBot.isAcceptingUserInput = true;

            var patient = new User(name, clockworkBot);
            var request = new Request(question, patient, clockworkBot);
            var answer = clockworkBot.Chat(request);
            return answer.Output;
        }
    }
}
