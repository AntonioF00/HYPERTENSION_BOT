using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class NegativeMessage
    {
        public List<string> Messages { get; }

        public NegativeMessage()
        {
            Messages = new List<string>();
            Messages.Add("no");   
            Messages.Add("n");    
            Messages.Add("sbagliato");   
            Messages.Add("errato");   
            Messages.Add("non sono corretti");  
            Messages.Add("non corretti");  
        }
    }
}
