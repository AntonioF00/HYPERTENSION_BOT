using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class InsertMessage
    {
        public List<string> Messages { get; }

        private static readonly List<string> _messages = new List<string>
        {
            "Perfetto quindi ho capito tutto!\nInserisco subito i tuoi dati!",
            "Il dottore sarà impaziente di vedere i tuoi dati!",
            "I dati sono stati inseriti\nSpero che il dottore capisca la mia calligrafia!",
            "Perfetto!"
        };

        public InsertMessage()
        {
            Messages = _messages;
        }
    }
}
