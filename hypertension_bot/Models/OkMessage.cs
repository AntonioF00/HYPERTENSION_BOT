using System.Collections.Generic;

namespace hypertension_bot.Models
{
    public class OkMessage
    {
        public List<string> Messages { get; }

        public OkMessage()
        {
            Messages = new List<string>();
            Messages.Add("Si");
            Messages.Add("si");
            Messages.Add("SI");
            Messages.Add("S");
            Messages.Add("s");
            Messages.Add("Perfetto");
            Messages.Add("perfetto");
            Messages.Add("PERFETTO");
            Messages.Add("vabene");
            Messages.Add("va bene");
            Messages.Add("Va bene");
            Messages.Add("Va Bene");
            Messages.Add("VABENE");
            Messages.Add("ok");
            Messages.Add("Ok");
            Messages.Add("OK");
            Messages.Add("OKY");
            Messages.Add("Oky");
            Messages.Add("oky");
            Messages.Add("YES");
            Messages.Add("yes");
            Messages.Add("Yes");
            Messages.Add("esatto");
            Messages.Add("ESATTO");
            Messages.Add("Esatto");
            Messages.Add("esattamente");
            Messages.Add("ESATTAMENTE");
            Messages.Add("Esattamente");
        }
    }
}
