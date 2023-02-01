using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class PressureMessage
    {
        public List<string> Messages { get; }
        public List<string> HowToMessages { get; }

        private readonly List<string> _messages = new List<string>()
        {
            "pressione",
        };        
        
        private readonly List<string> _HowTomessages = new List<string>()
        {
            "1.La pressione a domicilio andrebbe misurata una o due volte alla settima e comunque ogni 7 giorni prima di ogni visita del medico." +
            "\n2.Per le misurazioni meglio preferire i giorni di lavoro normali" +
            "\n3.Sarebbe ideale che quando si misura la pressione si facciano 2 misurazioni mattino e sera" +
            "\n4.Misurare la pressione dopo 5 minuti di riposo in posizione seduta e mantenendo 1 minuto di distanza tra le misurazioni" +
            "\n5.Meglio misurare la presione prima dell'assunzione di farmaci, se in trattamento" +
            "\n6.Evitare autoregolazione del dosaggio dei farmaci in base alle automisurazioni.",
        };

        public PressureMessage()
        {
            Messages = _messages;
            HowToMessages = _HowTomessages;
        }
    }
}
