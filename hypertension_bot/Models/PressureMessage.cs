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
            Messages.Add("Pressione");   
            Messages.Add("PRESSIONE");   
            Messages.Add("voglio inserire la pressione");   
            Messages.Add("Voglio inserire la pressione");   
            Messages.Add("inserire pressione");   
            Messages.Add("Inserire pressione");   
            Messages.Add("inserire la pressione");   
            Messages.Add("Inserire la pressione");   
            
        }
    }
}
