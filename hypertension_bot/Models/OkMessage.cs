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
            "perfe",
            "vaben",
            "ok",
            "y",
            "esat",
            "ben",
            "go"
        };

        public OkMessage()
        {
            Messages = _messages;
        }
    }
}
