using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class HelloMessage
    {
        public List<string> Messages { get; }
        public List<string> ReplyMessages { get; }
        public List<string> InitialMessages { get; }

        private static readonly List<string> _messages = new List<string>
        {
            "cia",
            "buongior",
            "buon gio",
            "buonase",
            "buona se",
            "we"
        };

        private static readonly List<string> _replyMessages = new List<string>
        {
            "Ciao",
            "Ciao! sono felice di vederti",
            "E' da un pò che non ci sentiamo",
            "Finalmente posso dire ch'è un BUONGIORNO, Ciao!",
        };        
        
        private static readonly List<string> _initialMessages = new List<string>
        {
            "Ciao! Mi chiamo Hypertension Bot e sono il chatbot per aiutarti a misurarti la pressione. " +
            "Puoi chiedermi come farlo correttamente, puoi chiedermi di registrare i tuoi valori di pressione minima, massima ed eventualmente " +
            "di frequenza cardiaca (TUTTI IN UN SOLO MESSAGGIO). Puoi anche chiedermi di visualizzare le medie settimanali, mensili e giornaliere." +
            "Coraggio, mettimi alla prova, chiedimi come ci si misura la pressione!"
        };

        public HelloMessage()
        {
            Messages = _messages;
            ReplyMessages = _replyMessages;
            InitialMessages = _initialMessages;
        }
    }
}
