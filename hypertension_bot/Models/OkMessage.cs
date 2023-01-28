using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class OkMessage
    {
        public List<string> Messages { get; }

        public OkMessage()
        {
            Messages = new List<string>();
            Messages.Add("si");
            Messages.Add("s");
            Messages.Add("perfetto");
            Messages.Add("vabene");
            Messages.Add("va bene");
            Messages.Add("ok");
            Messages.Add("oky");
            Messages.Add("yes");
            Messages.Add("esatto");
            Messages.Add("esattamente");
        }
    }
}
