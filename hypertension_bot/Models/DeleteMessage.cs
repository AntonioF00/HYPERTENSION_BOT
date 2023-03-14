using System.Collections.Generic;
using System.Text;

namespace hypertension_bot.Models
{
    public class DeleteMessage
    {
        public List<string> Messages { get; }
        public List<string> DeleteMessages { get; }

        private static readonly List<string> _messages = new List<string>
        {
            "Oh oh! vuoi elimare una misurazione? lascia che ti mostri le tue ultime misurazioni!",
            "Ecco le tue ultime misurazioni!",
            "Eliminiamo la misurazione prima che la possa vedere il medico! Queste sono le tue ultime misurazioni:",
            "Questo rimarrà un segreto tra me e te! il medico non lo verrà mai a sapere! ecco le tue ultime misurazioni:"
        };        
        
        private static readonly List<string> _deleteMessages = new List<string>
        {
            "Perfetto! Ho eliminato la misurazione!",
            "Agli ordini! il dottore non scoprirà nulla!",
            "Ho eliminato la misurazione!",
            "Misurazione eliminata! Spero di non aver lasciato tracce!"
        };

        public DeleteMessage()
        {
            Messages = _messages;
            DeleteMessages = _deleteMessages;
        }

        public string listMessage(Dictionary<string,object> list)
        {
            StringBuilder res = new StringBuilder();
            res.Append("Id | Sistolica | Diastolica | Frequenza cardiaca | data\n");
            int i = 0;
            foreach (var e in list)
            {
                res.Append($"{e.Value} | ");
                i++;

                if (i == 5)
                {
                    res.Append("\n");
                    i = 0;
                }
            }
            return res.ToString();
        }
    }
}
