using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class HelloMessage
    {
        public List<string> Messages { get; }
        public List<string> ReplyMessages { get; }

        public HelloMessage()
        {
            Messages = new List<string>();

            Messages.Add("ciao");
            Messages.Add("buongiorno");
            Messages.Add("buonasera");


            ReplyMessages = new List<string>();
            ReplyMessages.Add("Ciao");
            ReplyMessages.Add("Ciao!, sono felice di vederti");
            ReplyMessages.Add("E' da un pò che non ci sentiamo");
            ReplyMessages.Add("Finalmente posso dire ch'è un BUONGIORNO, Ciao");

        }
    }
}
