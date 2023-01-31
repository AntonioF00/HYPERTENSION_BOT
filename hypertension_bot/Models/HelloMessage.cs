using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class HelloMessage
    {
        public List<string> Messages { get; }
        public List<string> ReplyMessages { get; }

        private static readonly List<string> _messages = new List<string>
        {
            "ciao",
            "buongiorno",
            "buon giorno",
            "buonasera",
            "buona sera",
        };

        private static readonly List<string> _replyMessages = new List<string>
        {
            "Ciao",
            "Ciao!, sono felice di vederti",
            "E' da un pò che non ci sentiamo",
            "Finalmente posso dire ch'è un BUONGIORNO, Ciao",
        };

        public HelloMessage()
        {
            Messages = _messages;
            ReplyMessages = _replyMessages;
        }
    }
}
