﻿using hypertension_bot.Loggers;
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

        private static TelegramBotClient _telegramBot = new TelegramBotClient(Setting.Istance.Configuration.BotToken);

        private static readonly Datas _data = new();

        private static readonly HelloMessage _helloMessage = new();

        private static readonly ErrorMessage _errorMessage = new();

        private static Random _rnd = new();


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

                Console.WriteLine($"\n{me.Username} token : {Setting.Istance.Configuration.BotToken}");
                Console.WriteLine($"\n{me.Username} : online");
                Console.WriteLine($"\nHello! I'm {me.Username} and i'm your Bot!");

                Console.ReadKey();
                _cancellationTokenSource.Cancel();

            }
            catch(Exception ex)
            {
                LogHelper.Log($"{System.DateTime.Now} | {ex.ToString()}");
                Console.WriteLine($"\nBot : offline..error");
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
                    _data.SentMessage = await botClient.SendTextMessageAsync(
                                                                                chatId: _data.ChatId,
                                                                                text: $"{_helloMessage.ReplyMessages[_rnd.Next(4)]} {_data.FirstName}! ",
                                                                                cancellationToken: cancellationToken);
                    _data.SentMessage = await botClient.SendTextMessageAsync(
                                                                                chatId: _data.ChatId,
                                                                                text: $"{_data.FirstName}, ti va di dirmi i tuoi valori di oggi? \n(scrivimeli in questo modo.. ad esempio '120 30'... grazie!)",
                                                                                cancellationToken: cancellationToken);
                }
                else
                {
                    _data.SentMessage = await botClient.SendTextMessageAsync(
                                                                               chatId: _data.ChatId,
                                                                               text: $"{_errorMessage.Messages[_rnd.Next(6)]}",
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

