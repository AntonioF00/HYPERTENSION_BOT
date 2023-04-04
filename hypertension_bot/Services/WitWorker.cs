﻿using System;
using System.Text;
using Azure.Core;
using hypertension_bot.Settings;
using WitAi;
using WitAi.Models;

namespace hypertension_bot.Services
{
	public class WitWorker
	{
        public StringBuilder response;
        private string ACCESS_TOKEN = Setting.Istance.Configuration.WitToken;
        public WitWorker() {}

		public void Run(string message)
		{
			response = new();

            var actions = new WitActions();
            Wit client = new Wit(accessToken: ACCESS_TOKEN, actions: actions);

            var resp = client.Message(message);
            foreach (var res in resp.Entities)
            {
                var r = res.Value;
                foreach (var re in r)
                {
                    response.Append(re["value"].ToString().Replace('{', ' ').Replace('}', ' ').Trim());
                }
            }
        }
	}
}

