using hypertension_bot.Data;
using System.Text;

namespace hypertension_bot.Models
{
    public class ListMessage
    {
        public List<string> Messages { get; }
        private readonly DbController _dbController = new DbController();

        private static readonly List<string> _messages = new List<string>
        {
            "list",
            "elen",
            "le",
            "tut",
            "veder",
            "visuali",
            "mostr",
            "stamp"
        };
  
        public ListMessage()
        {
            Messages = _messages;
        }

        private string FormatListMessage(string name, string period, List<Dictionary<string, object>> list)
        {
            StringBuilder res = new StringBuilder();
            if (list.Count > 0)
            {
                res.AppendLine($"Elenco {period} di {name}:");
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

        public string MonthList(long id, string name)
        {
            var res = _dbController.getMeasurementMonthList(id);
            var s = FormatListMessage(name, "mensile", res);
            return s;
        }

        public string WeekList(long id, string name)
        {
            var res = _dbController.getMeasurementWeekList(id);
            var s = FormatListMessage(name, "settimanale", res);
            return s;
        }

        public string DayList(long id, string name)
        {
            var res = _dbController.getMeasurementDayList(id);
            var s = FormatListMessage(name, "giornaliero", res);
            return s;
        }
    }
}
