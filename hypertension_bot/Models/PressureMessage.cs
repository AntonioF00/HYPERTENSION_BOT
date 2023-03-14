using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class PressureMessage
    {
        public List<string> Messages { get; }
        public List<string> HowToMessages { get; }

        private readonly List<string> _messages = new List<string>()
        {
            "pressi",
        };        
        
        private readonly List<string> _HowTomessages = new List<string>()
        {
            "1). La pressione a domicilio andrebbe misurata una o due volte alla settima e comunque ogni 7 giorni prima di ogni visita del medico." +
            "\n2). Per le misurazioni meglio preferire i giorni di lavoro normali" +
            "\n3). Sarebbe ideale che quando si misura la pressione si facciano 2 misurazioni mattino e sera" +
            "\n4). Prima di iniziare il monitoraggio verifichi con il medico che lo strumento che utilizza sia accurato e valido; sono da preferire strumenti automatici da braccio" +
            "\n5). Le misurazioni sono da effettuare sempre nello stesso orario – al mattino e alla sera prima di assumere l’eventuale terapia per l’ipertensione"+
            "\n6). Procedura di misurazione:\r\n\t\t• Attendere qualche minuto in posizione seduta prima di effettuare le misurazioni. Evitare di effettuare le misurazioni dopo: pasti, fumo di sigarette, consumo di alcolici, sforzi fisici o stress emotivi.\r\n\t\t• Posizionare il bracciale 1-2 cm sopra la piega del gomito, sempre sullo stesso braccio. Durante le misurazioni restare in posizione seduta, comoda, con il braccio rilassato e appoggiato in modo che il bracciale si trovi all’altezza del cuore. Non parlare durante le misurazioni." +
            "\n7). Misurare la pressione dopo 5 minuti di riposo in posizione seduta e mantenendo 1 minuto di distanza tra le misurazioni" +
            "\n8). Meglio misurare la presione prima dell'assunzione di farmaci, se in trattamento" +
            "\n9). Evitare autoregolazione del dosaggio dei farmaci in base alle automisurazioni." +
            "\nINSERISCI LA TUA MISURAZIONE NELLA SEGUENTE MANIERA '120 80'" +
            "\nQUALORA VOLESSI INSERIRE ANCHE LA FREQUENZA CARDIACA INSERISCILA COME ULTIMO VALORE AD ESEMPIO '120 80 60'" +
            "\n",
        };

        public PressureMessage()
        {
            Messages = _messages;
            HowToMessages = _HowTomessages;
        }
    }
}
