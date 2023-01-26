using System.Collections.Generic;

namespace hypertension_bot.Intents
{
    public class HelloMessage
    {
        public List<string> Messages { get; }

        public HelloMessage()
        {
            Messages = new List<string>();
            Messages.Add("Ciao");
            Messages.Add("ciao");
            Messages.Add("CIAO");
            Messages.Add("buongiorno");
            Messages.Add("Buongiorno");
            Messages.Add("BUONGIORNO");
            Messages.Add("buonasera");
            Messages.Add("Buonasera");
            Messages.Add("BUONASERA");
        }
    }
}
