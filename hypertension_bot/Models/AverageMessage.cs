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
           return new string($"{Name} ho calcolato questi valori\nMedia mensile: Sistolica:{res[0]} Diastolica:{res[1]}\nA presto!");
        }

        public string calculateWeekAVG(long id, string Name)
        {
            var res = _dbController.CalculateWeekAVG(id);
            return new string($"{Name} ho calcolato questi valori\nMedia settimanale: Sistolica:{res[0]} Diastolica:{res[1]}\nA presto!");
        }

    }
}
