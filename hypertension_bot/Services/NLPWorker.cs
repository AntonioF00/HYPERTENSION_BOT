using System.Text;
using hypertension_bot.Settings;
using OpenAI_API.Models;
using Telegram.Bot.Types;
using LogHelper = hypertension_bot.Loggers.LogHelper;

namespace hypertension_bot.Services
{
    /// <summary>
    /// per sviluppare l'intelligenza artificiale mi sono basato sulle API di chatGPT
    /// Betalgo.OpenAI.GPT3
    /// https://liuhongbo.medium.com/how-to-use-chatgpt-api-in-c-d9133a3b8ef9
    /// https://rogerpincombe.com/openai-dotnet-api
    /// https://github.com/OkGoDoIt/OpenAI-API-dotnet/blob/master/README.md
    /// </summary>
    internal class NLPWorker
    {
        public StringBuilder res;
        public NLPWorker() { }

        public async Task RunAsync(string text)
        {
            try
            {
                res = new();
                var api = new OpenAI_API.OpenAIAPI(Setting.Istance.Configuration.GPT3Api);

                var chat = api.Chat.CreateConversation();
                chat.AppendUserInput(text);
                string response = await chat.GetResponseFromChatbot();
                res.AppendLine(response);

                //var result = await api.Completions.CreateCompletionAsync(text, temperature: 0.1);
            }
            catch (Exception ex)
            {
                LogHelper.Log($"{System.DateTime.Now} | Message: {ex.Message} | StackTrace: {ex.StackTrace}");
                res.Append(string.Empty);
            }
        }
    }
}
