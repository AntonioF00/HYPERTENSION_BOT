using hypertension_bot.Settings;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace hypertension_bot
{
    class program
    {
        private static TelegramBotClient _telegramBot = new TelegramBotClient(Setting.Istance.Configuration.BotToken);

        static void Main(string[] args)
        {
            var _receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new UpdateType[]
                {
                    UpdateType.Message,
                    UpdateType.EditedMessage,
                }
            };

            _telegramBot.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions);

            Console.ReadLine();
        }

        private static Task ErrorHandler(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3) => throw new NotImplementedException();

        private static async Task UpdateHandler(ITelegramBotClient bot, Update update, CancellationToken arg3)
        {
                if (update.Type == UpdateType.Message)
                {
                    if (update.Message.Type == MessageType.Text)
                    {
                        var text = update.Message.Text;
                        var id = update.Message.Chat.Id;
                        string? username = update.Message.Chat.Username;

                        Console.WriteLine($"username : {username} | id : {id} | text : {text}");
                    }
                }
        }
    }
}

