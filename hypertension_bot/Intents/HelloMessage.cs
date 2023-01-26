namespace hypertension_bot.Intents
{
    public class HelloMessage
    {
        private List<String> _messages;

        public List<string> Messages { get => _messages; set => _messages = new List<String> { "Ciao", "ciao", "CIAO", "buongiorno", "Buongiorno", "BUONGIORNO", "buonasera", "buonasera", "BUONASERA" }; }
    }
}
