using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class ThankMessage
    {
        public List<string> Messages { get; }
        public List<string> ReplyMessages { get; }

        public ThankMessage()
        {
            Messages = new List<string>();
            Messages.Add("grazie");   
            Messages.Add("Grazie");   
            Messages.Add("GRAZIE");   
            Messages.Add("Ti ringrazio");   
            Messages.Add("ti ringrazio");   
            Messages.Add("ok");   
            Messages.Add("Ok");   
            Messages.Add("OK");   
            Messages.Add("VA BENE");   
            Messages.Add("va bene");   
            Messages.Add("Va bene");   
            Messages.Add("Vabene");   
            Messages.Add("vabene");   
            Messages.Add("VABENE");   
            Messages.Add("perfetto");   
            Messages.Add("PERFETTO");   
            Messages.Add("Perfetto");

            ReplyMessages = new List<string>();
            ReplyMessages.Add("Non c'è bisogno che mi ringrazi!");
            ReplyMessages.Add("Di nulla!");
            ReplyMessages.Add("E' il mio dovere!");
            ReplyMessages.Add("E' un lavoro duro ma qualcuno dovrà pur farlo!");
            ReplyMessages.Add("Ci mancherebbe!");
        }
    }
}
