using System.Text;
using hypertension_bot.Settings;
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

                var result = await api.Completions.CreateCompletionAsync(text, temperature: 0.9);
                foreach (var r in result.Completions)
                {
                    res.AppendLine(r.Text.ToString());
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log($"{System.DateTime.Now} | Message: {ex.Message} | StackTrace: {ex.StackTrace}");
                res.Append(string.Empty);
            }
        }
    }
}
