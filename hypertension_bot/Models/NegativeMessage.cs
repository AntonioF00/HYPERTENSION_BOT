using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class NegativeMessage
    {
        public List<string> Messages { get; }

        private static readonly List<string> _messages = new List<string>
        {
            "no",
            "n",
            "sbagli",
            "erra",
            "cance",
            "negat"
        };

        public NegativeMessage()
        {
            Messages = _messages; 
        }
    }
}
