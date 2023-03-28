using hypertension_bot.Data;
using hypertension_bot.Settings;

namespace hypertension_bot.Models
{
    public class AverageMessage
    {
        private readonly DbController _dbController = new DbController();
        public List<string> Messages { get; }

        private static readonly List<string> _messages = new List<string>
        {
            "mensile",
            "mese",
            "mesi",
            "giorno",
            "giorni",
            "giornalieri",
            "giornaliero",
            "settimana",
            "settimanale",
            "settimane",
            "oggi",
        };

        public AverageMessage()
        {
            Messages = _messages;
        }

        private string FormatAverageMessage(string name, string period, Dictionary<string, object> res)
        {
            if (res.TryGetValue("systolic", out var systolicStr) &&
                int.TryParse(systolicStr.ToString(), out var sistolic))
            {
                var valoreMaxSi = Setting.Istance.Configuration.ValoreMaxSi;
                var valoreMinSi = Setting.Istance.Configuration.ValoreMinSi;
                var s = $"{name} ho trovato questi valori!\nMedia {period}:\nSistolica: {sistolic} mmHg\nDiastolica: {res["diastolic"]} mmHg\nFrequenza cardiaca: {res["heartrate"]} bpm\n" +
                        (sistolic >= valoreMaxSi || sistolic < valoreMinSi
                            ? "La tua media non mi piace!\nDovresti chiamare il medico!"
                            : "A presto!");
                return s;
            }
            else
            {
                return "Non ho trovato alcun valore! Mi dispiace...";
            }
        }

        public string calculateMonthAVG(long id, string name)
        {
            var res = _dbController.CalculateMonthAVG(id);
            var s = FormatAverageMessage(name, "mensile", res);
            return s;
        }

        public string calculateWeekAVG(long id, string name)
        {
            var res = _dbController.CalculateWeekAVG(id);
            var s = FormatAverageMessage(name, "settimanale", res);
            return s;
        }

        public string calculateDayAVG(long id, string name)
        {
            var res = _dbController.CalculateDayAVG(id);
            var s = FormatAverageMessage(name, "giornaliera", res);
            return s;
        }
    }
}
