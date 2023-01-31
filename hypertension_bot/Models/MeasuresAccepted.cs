using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class MeasuresAccepted
    {
        public List<string> Message { get; }

        private static readonly List<string> _messages = new List<string>
        {
            "Credo che i tuoi valori non siano corretti!",
            "Prova a ricontrollare i tuoi valori!\nIl medico è molto preoccupato!",
            "Qualcosa è andato storto\nI tuoi valori sono alle stelle!\nProva a rieffettuare la misurazione!"
        };

        public MeasuresAccepted()
        {
            Message = _messages;
        }
    }
}
