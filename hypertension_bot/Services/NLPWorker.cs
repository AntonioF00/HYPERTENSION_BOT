using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using hypertension_bot.Loggers;
using hypertension_bot.Settings;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Logging;
using LogHelper = hypertension_bot.Loggers.LogHelper;

namespace hypertension_bot.Services
{
    /// <summary>
    /// per sviluppare l'intelligenza artificiale mi sono basato sulle API di chatGPT
    /// Betalgo.OpenAI.GPT3
    /// https://liuhongbo.medium.com/how-to-use-chatgpt-api-in-c-d9133a3b8ef9
    /// https://rogerpincombe.com/openai-dotnet-api
    /// </summary>
    internal class NLPWorker
    {
        public StringBuilder res = new();
        public NLPWorker() { }

        public async Task RunAsync(string text)
        {
            try
            {
                var api = new OpenAI_API.OpenAIAPI(Setting.Istance.Configuration.GPT3Api);

                var result = await api.Completions.CreateCompletionAsync(text, temperature: 0.1);
                foreach (var r in result.Completions)
                {
                    res.AppendLine(r.Text);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log($"{System.DateTime.Now} | {ex.Message} |{ex.StackTrace}");
                res.Append(string.Empty);
            }
        }
    }
}
