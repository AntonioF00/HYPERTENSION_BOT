using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class ChartMessage
    {
        public List<string> Messages { get; }
        public List<string> ReplyMessages { get; }

        private static readonly List<string> _messages = new List<string>
        {
            "graf",
            "fot",
            "imma",
            "cha",
            "grap"
        };

        private static readonly List<string> _replyMessages = new List<string>
        {
            "Non sono molto bravo a disegnare! Eccoti dei grafici inerenti al tuo anadamento!",
            "Certo! Ecco quello che mi hai chiesto!"
        };

        public ChartMessage()
        {
            Messages = _messages;
            ReplyMessages = _replyMessages; 
        }
    }
}
