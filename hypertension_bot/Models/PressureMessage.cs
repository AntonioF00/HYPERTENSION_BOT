using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class PressureMessage
    {
        public List<string> Messages { get; }

        private readonly List<string> _messages = new List<string>()
        {
            "pressione",
            "voglio inserire la pressione",
            "inserire pressione",
            "inserire la pressione"
        };

        public PressureMessage()
        {
            Messages = _messages;
        }
    }
}
