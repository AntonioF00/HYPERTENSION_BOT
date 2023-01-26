using hypertension_bot.Loggers;
using hypertension_bot.Settings;
using Microsoft.ServiceBus.Messaging;
using System.Runtime.CompilerServices;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace hypertension_bot
{
    class program
    {
        static TelegramBotClient _telegramBot = new TelegramBotClient(Setting.Istance.Configuration.BotToken);

        static async Task Main(string[] args)
        {
            using var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };


            try
            {
                _telegramBot.StartReceiving(
                                            HandleUpdateAsync,
                                            HandleErrorAsync,
                                            receiverOptions,
                                            cancellationToken: cts.Token);

                var me = await _telegramBot.GetMeAsync();

                Console.ReadKey();
                cts.Cancel();

                //_telegramBot.StartReceiving();
                //_telegramBot.OnMessage += Bot_OnMessage;

            }
            catch(Exception ex)
            {
                LogHelper.Log($"{System.DateTime.Now} | {ex.ToString()}");
            }

            Console.ReadLine();
        }


        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }




        //public static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        //{
        //    if(e.Message.Type == MessageType.Text)
        //    {
        //        if(MessageType.Text.Equals("Ciao"))
        //        {
        //            _telegramBot.SendTextMessageAsync(e.Message.Chat.Id, $"Hello {e.Message.Chat.Username}");
        //        }
        //        else
        //        {
        //            _telegramBot.SendTextMessageAsync(e.Message.Chat.Id, $"Sorry i don't understand u...");
        //        }
        //    }
        //}
    }
}

