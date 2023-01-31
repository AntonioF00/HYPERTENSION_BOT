using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class ThankMessage
    {
        public List<string> Messages { get; }
        public List<string> ReplyMessages { get; }

        private readonly List<string> _messages = new List<string>()
        {
            "grazie",
            "ti ringrazio",
            "ok",
            "va bene",
            "vabene",
            "perfetto",
            "grazie talktalk",
            "ti ringrazio talktalk",
            "ok talktalk",
            "va bene talktalk",
            "vabene talktalk",
            "perfetto talktalk"
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
