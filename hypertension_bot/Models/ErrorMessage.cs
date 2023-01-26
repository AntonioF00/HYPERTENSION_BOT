using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class ErrorMessage
    {
        public List<string> Messages { get; }

        public ErrorMessage()
        {
            Messages = new List<string>();
            Messages.Add("Non credo di aver capito!");
            Messages.Add("Potresti ripetere...oggi non sono proprio attento!");
            Messages.Add("Piano piano non ti ho capito!");
            Messages.Add("Scusami...non ho capito!");
            Messages.Add("Ieri sera non avrei dovuto far così tardi... \nnon ho capito, potresti ripetere?");
            Messages.Add("Oh oh...\nQualcosa dev'essere andato storto!\nForse quel bicchiere di vino non mi ha fatto bene!\nPotresti ripetere? non ho capito!");
        }
    }
}
