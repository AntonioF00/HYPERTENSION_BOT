using hypertension_bot.Data;
using hypertension_bot.Settings;
using System.Collections;
using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class AverageMessage
    {
        private readonly DbController _dbController = new DbController();

        public string calculateMonthAVG(long id, string Name)
        {
           string s;
           bool isEmpty;
           var res = _dbController.CalculateMonthAVG(id);
           using (var dictionaryEnum = res.GetEnumerator())
           {
             isEmpty = !dictionaryEnum.MoveNext();
           }
           if (!isEmpty)
           {
               var sistolic = int.Parse(string.Format("{0}", res["systolic"]));
               s = (sistolic >= Setting.Istance.Configuration.ValoreMaxSi || sistolic < Setting.Istance.Configuration.ValoreMinSi)
                                                                                 ? $"{Name} ho trovato questi valori!\nMedia mensile:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nFrequenza cardiaca : {res["heartrate"]} bpm\nLa tua media non mi piace!\nDovresti chiamare il medico!"
                                                                                 : $"{Name} ho trovato questi valori!\nMedia mensile:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nFrequenza cardiaca : {res["heartrate"]} bpm\nA presto!";
           }
           else
                s = new string("non ho trovato alcun valore! mi dispiace...");
           return s; 
        }

        public string calculateWeekAVG(long id, string Name)
        {
            string s;
            bool isEmpty;
            var res = _dbController.CalculateWeekAVG(id);
            using (var dictionaryEnum = res.GetEnumerator())
            {
                isEmpty = !dictionaryEnum.MoveNext();
            }
            if (!isEmpty)
            {
                var sistolic = int.Parse(string.Format("{0}", res["systolic"]));
                s = (sistolic >= Setting.Istance.Configuration.ValoreMaxSi || sistolic < Setting.Istance.Configuration.ValoreMinSi)
                                                                                  ? $"{Name} ho trovato questi valori!\nMedia settimanale:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nFrequenza cardiaca : {res["heartrate"]} bpm\nLa tua media è molto elevata!\nDovresti chiamare il medico!"
                                                                                  : $"{Name} ho trovato questi valori!\nMedia settimanale:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nFrequenza cardiaca : {res["heartrate"]} bpm\nA presto!";

            }
            else
                s = new string("non ho trovato alcun valore! mi dispiace...");
            return s;
        }

        public string calculateDayAVG(long id, string Name)
        {
            string s;
            bool isEmpty;
            var res = _dbController.CalculateDayAVG(id);
            using (var dictionaryEnum = res.GetEnumerator())
            {
                isEmpty = !dictionaryEnum.MoveNext();
            }
            if (!isEmpty)
            {

                var sistolic = int.Parse(string.Format("{0}", res["systolic"]));
                s = (sistolic >= Setting.Istance.Configuration.ValoreMaxSi || sistolic < Setting.Istance.Configuration.ValoreMinSi)
                                                                              ? $"{Name} ho trovato questi valori!\nMedia giornaliera:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nFrequenza cardiaca : {res["heartrate"]} bpm\nLa tua media è molto elevata!\nDovresti chiamare il medico!"
                                                                              : $"{Name} ho trovato questi valori!\nMedia giornaliera:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nFrequenza cardiaca : {res["heartrate"]} bpm\nA presto!";
            }
            else
                s = new string("non ho trovato alcun valore! mi dispiace...");
            return s;
        }
    }
}
