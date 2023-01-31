using hypertension_bot.Data;
using hypertension_bot.Settings;
using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class AverageMessage
    {
        private readonly DbController _dbController = new DbController();

        public string calculateMonthAVG(long id, string Name)
        {
           string s;
           var res = _dbController.CalculateMonthAVG(id);
           var sistolic = int.Parse(string.Format("{0}", res["systolic"]));
           _ = (sistolic >= Setting.Istance.Configuration.ValoreMaxSi)
                                                                       ? s = $"{Name} ho trovato questi valori!\nMedia mensile:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nLa tua media è molto elevata!\nDovresti chiamare il medico!"
                                                                       : s = $"{Name} ho trovato questi valori!\nMedia mensile:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nA presto!";

           return s; 
        }

        public string calculateWeekAVG(long id, string Name)
        {
            string s;
            var res = _dbController.CalculateWeekAVG(id);
            var sistolic = int.Parse(string.Format("{0}", res["systolic"]));
            _ = (sistolic >= Setting.Istance.Configuration.ValoreMaxSi)
                                                                        ? s = $"{Name} ho trovato questi valori!\nMedia settimanale:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nLa tua media è molto elevata!\nDovresti chiamare il medico!"
                                                                        : s = $"{Name} ho trovato questi valori!\nMedia settimanale:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nA presto!";

            return s;
        }

        public string calculateDayAVG(long id, string Name)
        {
            string s;
            var res = _dbController.CalculateWeekAVG(id);
            var sistolic = int.Parse(string.Format("{0}", res["systolic"]));
            _ = (sistolic >= Setting.Istance.Configuration.ValoreMaxSi)
                                                                        ? s = $"{Name} ho trovato questi valori!\nMedia giornaliera:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nLa tua media è molto elevata!\nDovresti chiamare il medico!"
                                                                        : s = $"{Name} ho trovato questi valori!\nMedia giornaliera:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nA presto!";

            return s;
        }
    }
}
