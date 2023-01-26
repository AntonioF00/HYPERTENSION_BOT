using hypertension_bot.Loggers;
using hypertension_bot.Models;
using hypertension_bot.Settings;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Message = Telegram.Bot.Types.Message;
using Poll = Telegram.Bot.Types.Poll;

namespace hypertension_bot
{
    class program
    {
        /// <summary>
        /// https://github.com/ZETALVX/Telegram.Bot/blob/main/Program.cs
        /// </summary>


        static TelegramBotClient _telegramBot = new TelegramBotClient(Setting.Istance.Configuration.BotToken);

        static readonly Datas _data;

        static async Task Main(string[] args)
        {
            using var _cancellationTokenSource = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };

            try
            {
                _telegramBot.StartReceiving(HandleUpdateAsync,
                                            HandleErrorAsync,
                                            receiverOptions,
                                            cancellationToken: _cancellationTokenSource.Token);

                var me = await _telegramBot.GetMeAsync();

                Console.ReadKey();
                _cancellationTokenSource.Cancel();

            }
            catch(Exception ex)
            {
                LogHelper.Log($"{System.DateTime.Now} | {ex.ToString()}");
                _cancellationTokenSource.Cancel();
            }

            Console.ReadLine();
        }


        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Type != UpdateType.Message)
                return;
            // Only process text messages
            if (update.Message!.Type != MessageType.Text)
                return;

            //set variables
            _data.ChatId        = update.Message.Chat.Id;
            _data.MessageText   = update.Message.Text;
            _data.MessageId     = update.Message.MessageId;
            _data.FirstName     = update.Message.From.FirstName;
            _data.LastName      = update.Message.From.LastName;
            _data.Id            = update.Message.From.Id;


            //if message is Hello .. bot answer Hello + name of user.
            if (_data.MessageText.Equals("hello"))
            {
                // Echo received message text
                _data.SentMessage = await botClient.SendTextMessageAsync(
                chatId: _data.ChatId,
                text: "Hello " + _data.FirstName + " " + _data.LastName + "",

                cancellationToken: cancellationToken);
            }

            //if message is "poll" .. create a poll.
            if (_data.MessageText.Equals("poll"))
            {
                //save the poll id message
                _data.PollId = _data.MessageId + 1;

                Console.WriteLine($"\nPoll number: {_data.PollId}!");
                Message pollMessage = await botClient.SendPollAsync(
                chatId: _data.ChatId,
                question: "How are you?",
                options: new[]
                {
                    "Good!",
                    "I could be better.."
                },
                cancellationToken: cancellationToken);
            }
            //if message is "close poll" .. close the pool.
            if (_data.MessageText == "close poll")
            {
                Console.WriteLine($"\nPoll number {_data.PollId} is close!");
                Poll poll = await botClient.StopPollAsync(
                chatId: _data.ChatId,
                messageId: _data.PollId,
                cancellationToken: cancellationToken);
            }
        }

        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _   => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            LogHelper.Log($"{System.DateTime.Now} | {ErrorMessage}");
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

