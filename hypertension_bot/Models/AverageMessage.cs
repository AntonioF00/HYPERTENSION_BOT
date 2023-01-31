using hypertension_bot.Data;
using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class AverageMessage
    {
        private readonly DbController _dbController = new DbController();

        public string calculateMonthAVG(long id, string Name)
        {
           var res = _dbController.CalculateMonthAVG(id);
           return new string($"{Name} ho trovato questi valori!\nMedia mensile:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nA presto!");
        }

        public string calculateWeekAVG(long id, string Name)
        {
            var res = _dbController.CalculateWeekAVG(id);
            return new string($"{Name} ho trovato questi valori!\nMedia settimanale:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nA presto!");
        }

        public string calculateDayAVG(long id, string Name)
        {
            var res = _dbController.CalculateWeekAVG(id);
            return new string($"{Name} ho trovato questi valori!\nMedia giornaliera:\nSistolica: {res["systolic"]} mmHg\nDiastolica: {res["diastolic"]} mmHg\nA presto!");
        }
    }
}
