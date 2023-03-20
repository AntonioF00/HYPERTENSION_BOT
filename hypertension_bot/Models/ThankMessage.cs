using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class ThankMessage
    {
        public List<string> Messages { get; }
        public List<string> ReplyMessages { get; }

        private readonly List<string> _messages = new List<string>()
        {
            "graz",
            "ti ringraz",
            "ok",
            "va ben",
            "vaben",
            "perfet",
            "genti",
            "teso",
            "amo",
            "brav"
        };

        private readonly List<string> _replyMessages = new List<string>
        {
            "Non c'è bisogno che mi ringrazi!",
            "Di nulla!",
            "E' il mio dovere!",
            "E' un lavoro duro ma qualcuno dovrà pur farlo!",
            "Ci mancherebbe!"
        };

        public ThankMessage()
        {
            Messages = _messages;
            ReplyMessages = _replyMessages;
        }
    }
}
