﻿using hypertension_bot.Intents;
using hypertension_bot.Loggers;
using hypertension_bot.Models;
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
        /// <summary>
        /// https://github.com/ZETALVX/Telegram.Bot/blob/main/Program.cs
        /// </summary>


        static TelegramBotClient _telegramBot = new TelegramBotClient(Setting.Istance.Configuration.BotToken);

        static readonly Datas _data = new();

        static HelloMessage _helloMessage = new();
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

                Console.WriteLine($"\nHello! I'm {me.Username} and i'm your Bot!");

                Console.ReadKey();
                _cancellationTokenSource.Cancel();

            }
            catch(Exception ex)
            {
                LogHelper.Log($"{System.DateTime.Now} | {ex.ToString()}");
            }

            Console.ReadLine();
        }


        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
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
                if (_helloMessage.Messages.Contains(_data.MessageText))
                {
                    // Echo received message text
                    _data.SentMessage = await botClient.SendTextMessageAsync(
                    chatId: _data.ChatId,
                    text: "Hello " + _data.FirstName + " " + _data.LastName + "",

                    cancellationToken: cancellationToken);
                }
                else
                {
                    _data.SentMessage = await botClient.SendTextMessageAsync(
                   chatId: _data.ChatId,
                   text: "scusami " + _data.FirstName + " " + _data.LastName + " non ti ho capito!",

                   cancellationToken: cancellationToken);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log($"{System.DateTime.Now} | {ex.ToString()}");
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
    }
}

