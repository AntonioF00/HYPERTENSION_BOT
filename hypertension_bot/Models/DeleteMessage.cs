using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class ErrorMessage
    {
        public List<string> Messages { get; }

        private static readonly List<string> _errorMessages = new List<string>
        {
            "Non credo di aver capito!",
            "Potresti ripetere...oggi non sono proprio attento!",
            "Piano piano non ti ho capito!",
            "Scusami...non ho capito!",
            "Ieri sera non avrei dovuto far così tardi...\nnon ho capito, potresti ripetere?",
            "Oh oh...\nQualcosa dev'essere andato storto!\nForse quel bicchiere di vino non mi ha fatto bene!\nPotresti ripetere? non ho capito!",
        };

        public ErrorMessage()
        {
            Messages = _errorMessages;
        }
    }
}
