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
           var res = _dbController.CalculateMonthAVG(id);
           var sistolic = int.Parse(string.Format("{0}", res["systolic"]));
           string s = (sistolic >= Setting.Istance.Configuration.ValoreMaxSi)
                                                                             ? $"{Name} ho trovato questi valori!\nMedia mensile:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nFrequenza cardiaca : {res["heartrate"]} bpm\nLa tua media è molto elevata!\nDovresti chiamare il medico!"
                                                                             : $"{Name} ho trovato questi valori!\nMedia mensile:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nA presto!";
           return s; 
        }

        public string calculateWeekAVG(long id, string Name)
        {
            var res = _dbController.CalculateWeekAVG(id);
            var sistolic = int.Parse(string.Format("{0}", res["systolic"]));
            string s = (sistolic >= Setting.Istance.Configuration.ValoreMaxSi)
                                                                              ? $"{Name} ho trovato questi valori!\nMedia settimanale:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nFrequenza cardiaca : {res["heartrate"]} bpm\nLa tua media è molto elevata!\nDovresti chiamare il medico!"
                                                                              : $"{Name} ho trovato questi valori!\nMedia settimanale:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nA presto!";
            return s;
        }

        public string calculateDayAVG(long id, string Name)
        {
            var res = _dbController.CalculateWeekAVG(id);
            var sistolic = int.Parse(string.Format("{0}", res["systolic"]));
            string s = (sistolic >= Setting.Istance.Configuration.ValoreMaxSi)
                                                                              ? $"{Name} ho trovato questi valori!\nMedia giornaliera:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nFrequenza cardiaca : {res["heartrate"]} bpm\nLa tua media è molto elevata!\nDovresti chiamare il medico!"
                                                                              : $"{Name} ho trovato questi valori!\nMedia giornaliera:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nA presto!";
            return s;
        }
    }
}
