using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class InsertMessage
    {
        public List<string> Messages { get; }

        public InsertMessage()
        {
            Messages = new List<string>();
            Messages.Add("Perfetto quindi ho capito tutto!\nInserisco subito i tuoi dati!");
            Messages.Add("Il dottore sarà impaziente di vedere i tuoi dati!");
            Messages.Add("I dati sono stati inseriti\nSpero che il dottore capisca la mia calligrafia!");
            Messages.Add("Perfetto!");
        }
    }
}
