using hypertension_bot.Loggers;
using hypertension_bot.Settings;
using System.Runtime.CompilerServices;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace hypertension_bot
{
    class program
    {
       static object _telegramBot = new TelegramBotClient(Setting.Istance.Configuration.BotToken);

        static void Main(string[] args)
        {
            try
            {
                _telegramBot.StartReceiving();
                _telegramBot.OnMessage += Bot_OnMessage;

            }catch(Exception ex)
            {
                LogHelper.Log($"{System.DateTime.Now} | {ex.ToString()}");
            }

            Console.ReadLine();
        }

        public static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if(e.Message.Type == MessageType.Text)
            {
                if(MessageType.Text.Equals("Ciao"))
                {
                    _telegramBot.SendTextMessageAsync(e.Message.Chat.Id, $"Hello {e.Message.Chat.Username}");
                }
                else
                {
                    _telegramBot.SendTextMessageAsync(e.Message.Chat.Id, $"Sorry i don't understand u...");
                }
            }
        }
    }
}

