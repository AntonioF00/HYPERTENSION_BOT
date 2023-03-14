using hypertension_bot.Data;
using hypertension_bot.Settings;

namespace hypertension_bot.Models
{
    public class AverageMessage
    {
        private readonly DbController _dbController = new DbController();

        public string calculateMonthAVG(long id, string name)
        {
            var res = _dbController.CalculateMonthAVG(id);
            if (res.TryGetValue("systolic", out var systolicStr) &&
                int.TryParse(systolicStr.ToString(), out var sistolic))
            {
                var valoreMaxSi = Setting.Istance.Configuration.ValoreMaxSi;
                var valoreMinSi = Setting.Istance.Configuration.ValoreMinSi;
                var s = $"{name} ho trovato questi valori!\nMedia mensile:\nSistolica: {sistolic} mmHg\nDiastolica: {res["diastolic"]} mmHg\nFrequenza cardiaca: {res["heartrate"]} bpm\n" +
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

        public string calculateWeekAVG(long id, string name)
        {
            var res = _dbController.CalculateWeekAVG(id);
            if (res.TryGetValue("systolic", out var systolicStr) &&
                int.TryParse(systolicStr.ToString(), out var sistolic))
            {
                var valoreMaxSi = Setting.Istance.Configuration.ValoreMaxSi;
                var valoreMinSi = Setting.Istance.Configuration.ValoreMinSi;
                var s = $"{name} ho trovato questi valori!\nMedia settimanale:\nSistolica: {sistolic} mmHg\nDiastolica: {res["diastolic"]} mmHg\nFrequenza cardiaca: {res["heartrate"]} bpm\n" +
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

        public string calculateDayAVG(long id, string name)
        {
            var res = _dbController.CalculateDayAVG(id);
            if (res.TryGetValue("systolic", out var systolicStr) &&
                int.TryParse(systolicStr.ToString(), out var sistolic))
            {
                var valoreMaxSi = Setting.Istance.Configuration.ValoreMaxSi;
                var valoreMinSi = Setting.Istance.Configuration.ValoreMinSi;
                var s = $"{name} ho trovato questi valori!\nMedia giornaliera:\nSistolica: {sistolic} mmHg\nDiastolica: {res["diastolic"]} mmHg\nFrequenza cardiaca: {res["heartrate"]} bpm\n" +
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
    }
}
