using hypertension_bot.Data;
using hypertension_bot.Loggers;
using hypertension_bot.Models;
using hypertension_bot.Services;
using hypertension_bot.Settings;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace hypertension_bot
{
    class program
    {
        private static TelegramBotClient _telegramBot = new TelegramBotClient(Setting.Istance.Configuration.BotToken);
        private static readonly Datas _data = new(); 
        private static readonly DbController _dbController = new();
        private static readonly List<ResearcherWorker> _researcherWorker = new();

        static async Task Main(string[] args)
        {
            using var _cancellationTokenSource = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions { AllowedUpdates = { } };
            try
            {
                _telegramBot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken: _cancellationTokenSource.Token);
                var me = await _telegramBot.GetMeAsync();

                Console.WriteLine($"\n{me.Username} token: {Setting.Istance.Configuration.BotToken}\n{me.Username}: online\nHello! I'm {me.Username} and i'm your Bot!");
                Console.ReadKey();
                _cancellationTokenSource.Cancel();
            }
            catch(Exception ex)
            {
                LogHelper.Log($"{System.DateTime.Now} | {ex.Message} |{ex.StackTrace}");
                Console.WriteLine($"\nBot : offline..error");
                _cancellationTokenSource.Cancel();
            }
            Console.ReadLine();
        }
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            List<string> message = new List<string>();

            if (update.Type != UpdateType.Message) 
                return;
            if (update.Message!.Type != MessageType.Text)
                return;

            ///inserisco l'utente nel database, controllando se vi è già
            _dbController.InsertUser((int)update.Message.From.Id);

            ///creo un nuovo researcherWorker per id
            _researcherWorker.Add(new ResearcherWorker((int)update.Message.From.Id, botClient));

            ///porte dell'inferno
            Worker _worker = new Worker();
            _worker.setting(botClient, cancellationToken, (int)update.Message.From.Id, update.Message.Chat.Id);

            ///cerco il contesto del messaggio e riporto una lista contenente i messaggi di ritorno
            message = await _researcherWorker.Find(x => x._id.Equals((int)update.Message.From.Id)).FindResponse(update.Message.Chat.Id,
                                                                                                                update.Message.Text.ToLower(),
                                                                                                                update.Message.From.FirstName,
                                                                                                                (int)update.Message.From.Id);
            ///analizzo e scorro la lista dei messaggi e li riporto all'utente
            foreach(var m in message)
            {
                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id,
                                                                         text: m,
                                                                         cancellationToken: cancellationToken);
            }
        }
        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                  _ => exception.ToString()
            };
            LogHelper.Log($"{System.DateTime.Now} | {ErrorMessage}");
            return Task.CompletedTask;
        }
    }
}