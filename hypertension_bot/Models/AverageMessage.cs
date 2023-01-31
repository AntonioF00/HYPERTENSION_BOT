using hypertension_bot.Data;
using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class AverageMessage
    {
        private readonly DbController _dbController = new DbController();

        public string calculateAVG(long id, string Name)
        {
           var res = _dbController.CalculateAVG(id);
           return new string($"{Name} ho calcolato questi valori\nMedia mensile: Sistolica:{res["monthsystolic"]} Diastolica:{res["monthdiastolic"]}\nA presto!");
        }
    }
}
