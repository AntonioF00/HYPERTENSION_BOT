using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class ExportMessage
    {
        public List<string> Messages { get; }
        public List<string> ReplyMessages { get; }

        private static readonly List<string> _messages = new List<string>
        {
            "espo",
            "exp",
            "inv",
            "man"
        };

        private static readonly List<string> _replyMessages = new List<string>
        {
            "Certo! invierò per mail le tue misurazioni al medico!",
            "Va bene! manderò una email al medico con le tue misurazioni!",
            "Ho inviato una email al medico con le tue misurazioni!\nSpero che possa capire la mia calligrafia!"
        };

        public ExportMessage()
        {
            Messages = _messages;
            ReplyMessages = _replyMessages; 
        }
    }
}
