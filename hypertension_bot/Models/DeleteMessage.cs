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

        public string listMessage(List<Dictionary<string,object>> list)
        {
            StringBuilder res = new StringBuilder();
            if (list.Count > 0)
            {
                res.AppendLine("Id | Sistolica | Diastolica | Frequenza cardiaca | data");
                int i = 0;
                foreach (var e in list)
                {
                    foreach (var v in e)
                    {
                        res.AppendFormat("{0} | ", v.Value);

                        if (++i % 5 == 0)
                        {
                            res.AppendLine();
                        }
                    }
                }
            }
            else
            {
                res.Append("Per visualizzare le misurazioni, inserirne prima una!");
            }
            return res.ToString();
        }
    }
}
