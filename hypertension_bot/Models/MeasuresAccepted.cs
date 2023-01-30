using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class MeasuresAccepted
    {
        public List<string> Message { get; }

        public MeasuresAccepted()
        {
            Message = new List<string>();
            Message.Add("Credo che i tuoi valori non siano corretti!");   
            Message.Add("Prova a ricontrollare i tuoi valori!\nIl medico è molto preoccupato!");   
            Message.Add("Qualcosa è andato storto\nI tuoi valori sono alle stelle!\nProva a rieffettuare la misurazione!");   
        }
    }
}
