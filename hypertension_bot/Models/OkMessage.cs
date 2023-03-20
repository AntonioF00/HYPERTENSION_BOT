using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class OkMessage
    {
        public List<string> Messages { get; }

        private readonly List<string> _messages = new List<string>
        {
            "si",
            "s",
            "perfet",
            "vaben",
            "ok",
            "oky",
            "yes",
            "esat",
            "ben",
        };

        public OkMessage()
        {
            Messages = _messages;
        }
    }
}
