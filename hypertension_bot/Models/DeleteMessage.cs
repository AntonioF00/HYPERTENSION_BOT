using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class DeleteMessage
    {
        public List<string> Messages { get; }

        private static readonly List<string> _deleteMessages = new List<string>
        {
            "Oh oh! vuoi elimare una misurazione? lascia che ti mostri le tue ultime misurazioni!",
            "Ecco le tue ultime misurazioni!",
            "Eliminiamo la misurazione prima che la possa vedere il medico! Queste sono le tue ultime misurazioni:",
            "Questo rimarrà un segreto tra me e te! il medico non lo verrà mai a sapere! ecco le tue ultime misurazioni:"
        };

        public DeleteMessage()
        {
            Messages = _deleteMessages;
        }
    }
}
