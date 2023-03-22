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

namespace hypertension_bot.Services
{
    /// <summary>
    /// per sviluppare l'intelligenza artificiale mi sono basato sulle API di chatGPT
    /// Betalgo.OpenAI.GPT3
    /// https://liuhongbo.medium.com/how-to-use-chatgpt-api-in-c-d9133a3b8ef9
    /// </summary>
    internal class NLPWorker
    {
        public string res { get; set; }
        public NLPWorker() { }

        public async Task RunAsync(string text)
        {
            var gpt3 = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = Setting.Istance.Configuration.GPT3Api
            });

            var completionResult = await gpt3.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = text,
                Temperature = 0.5F,
                MaxTokens = 100,
                N = 3
            });

            if (completionResult.Successful)
            {
                foreach (var choice in completionResult.Choices)
                {
                    res = choice.Text;
                }
            }
            else
            {
                if (completionResult.Error == null)
                {
                    res = "Non credo di aver capito!";
                    throw new Exception("Unknown Error");
                }
                LogHelper.Log($"{completionResult.Error.Code}: {completionResult.Error.Message}");
            }
        }
    }
}
