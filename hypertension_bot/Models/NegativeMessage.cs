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
            Messages.Add("No");   
            Messages.Add("N");   
            Messages.Add("n");   
            Messages.Add("NO");   
            Messages.Add("sbagliato");   
            Messages.Add("Sbagliato");   
            Messages.Add("SBAGLIATO");   
            Messages.Add("errato");   
            Messages.Add("Errato");   
            Messages.Add("ERRATO");  
            Messages.Add("non sono corretti");  
            Messages.Add("Non sono corretti");  
            Messages.Add("Non corretti");  
            Messages.Add("non corretti");  
        }
    }
}
