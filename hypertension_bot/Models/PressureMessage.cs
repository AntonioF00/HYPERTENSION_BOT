using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class PressureMessage
    {
        public List<string> Messages { get; }

        public PressureMessage()
        {
            Messages = new List<string>();
            Messages.Add("pressione");    
            Messages.Add("voglio inserire la pressione");   
            Messages.Add("inserire pressione");   
            Messages.Add("inserire la pressione");   
        }
    }
}
