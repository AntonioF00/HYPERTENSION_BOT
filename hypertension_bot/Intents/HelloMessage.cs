namespace hypertension_bot.Intents
{
    public class HelloMessage
    {
        public List<string> Messages
        {
            set
            {
                Messages = new List<string>
                {
                    "Ciao",
                    "ciao",
                    "CIAO",
                    "buongiorno",
                    "Buongiorno",
                    "BUONGIORNO",
                    "buonasera",
                    "Buonasera",
                    "BUONASERA"
                };
            }
            get
            { return Messages; }
        }
    }
}
