﻿using System.Collections.Generic;

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
            Messages.Add("YES");
            Messages.Add("yes");
            Messages.Add("Yes");
        }
    }
}
