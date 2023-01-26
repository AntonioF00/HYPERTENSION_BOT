namespace hypertension_bot.Intents
{
    public class HelloMessage
    {
        private string[]? _messages;
        public string[] Messages { get => _messages; set => _messages = new string[] { "Ciao", "ciao", "CIAO", "buongiorno", "Buongiorno", "BUONGIORNO", "buonasera", "buonasera", "BUONASERA" }; }
    }
}
