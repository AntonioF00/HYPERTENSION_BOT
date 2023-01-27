using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using hypertension_bot.Data;
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
        private static TelegramBotClient _telegramBot = new TelegramBotClient(Setting.Istance.Configuration.BotToken);

        private static readonly Datas _data = new();

        private static readonly HelloMessage _helloMessage = new();

        private static readonly ErrorMessage _errorMessage = new();

        private static readonly InsertMessage _insertMessage = new();

        private static readonly OkMessage _oKMessage = new();

        private static readonly NegativeMessage _negativeMessage = new();

        private static readonly PressureMessage _pressureMessage = new();

        private static readonly ThankMessage _thankMessage = new();

        private static readonly DbController _dbController = new();

        private static int _diastolic;

        private static int _sistolic;

        private static bool _done;

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
                _cancellationTokenSource.Cancel();
            }

            Console.ReadLine();
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var _unknown = false;
            
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

                ///controllo della misurazione periodica,
                ///se l'ultima misurazione è stata effettuata due o piu' 
                ///giorni fa, il bot invia un messaggio di promemoria all'utente.

                if (_thankMessage.Messages.Contains(_data.MessageText))
                {
                    _unknown = true;
                    _data.SentMessage = await botClient.SendTextMessageAsync(
                                                                             chatId: _data.ChatId,
                                                                             text: $"{_thankMessage.ReplyMessages[_rnd.Next(4)]}",
                                                                             cancellationToken: cancellationToken);
                }

                if (_oKMessage.Messages.Contains(_data.MessageText) && _done)
                {
                    _data.Done = false;
                    _unknown = true;
                    _data.SentMessage = await botClient.SendTextMessageAsync(
                                                                             chatId: _data.ChatId,
                                                                             text: $"{_insertMessage.Messages[_rnd.Next(4)]}\nA presto {_data.FirstName}!\nData : {System.DateOnly.FromDateTime(System.DateTime.Now)}",
                                                                             cancellationToken: cancellationToken);
                    //inserisco i dati nel database
                    _dbController.InsertMeasures(_diastolic.ToString(),_sistolic.ToString());

                }

                if (_negativeMessage.Messages.Contains(_data.MessageText) && _done)
                {
                    _data.Done = false;
                    _unknown = true;
                    _data.SentMessage = await botClient.SendTextMessageAsync(
                                                                             chatId: _data.ChatId,
                                                                             text: $"{_errorMessage.Messages[_rnd.Next(4)]}\n{_data.FirstName} prova a reinserire i dati!",
                                                                             cancellationToken: cancellationToken);
                }

                if (_helloMessage.Messages.Contains(_data.MessageText) || _pressureMessage.Messages.Contains(_data.MessageText))
                {
                    _unknown = true;
                    _data.SentMessage = await botClient.SendTextMessageAsync(
                                                                             chatId: _data.ChatId,
                                                                             text: $"{_helloMessage.ReplyMessages[_rnd.Next(4)]} {_data.FirstName}! ",
                                                                             cancellationToken: cancellationToken);

                    _data.SentMessage = await botClient.SendTextMessageAsync(
                                                                             chatId: _data.ChatId,
                                                                             text: $"{_data.FirstName}, ti va di dirmi i tuoi valori di oggi? \n(scrivimeli in questo modo...\nad esempio '120 30'...\nGRAZIE!)",
                                                                             cancellationToken: cancellationToken);
                }

                if (!string.IsNullOrEmpty(_data.MessageText))
                {
                    bool success = int.TryParse(new string(_data.MessageText
                                                .SkipWhile(x => !char.IsDigit(x))
                                                .TakeWhile(x => char.IsDigit(x))
                                                .ToArray()), out _diastolic);

                    if (_diastolic != 0 && success)
                    {
                        var mess = _data.MessageText.Replace(_diastolic.ToString(),"");

                        success = int.TryParse(new string(mess
                                               .SkipWhile(x => !char.IsDigit(x))
                                               .TakeWhile(x => char.IsDigit(x))
                                               .ToArray()), out _sistolic);

                        if (_sistolic != 0 && success)
                        {
                            _done = true;
                            _unknown = true;
                            _data.SentMessage = await botClient.SendTextMessageAsync(
                                                                                     chatId: _data.ChatId,
                                                                                     text: $"Diastolica : {_diastolic}\nSistolica : {_sistolic}\nSono corretti?",
                                                                                     cancellationToken: cancellationToken);
                        }
                    }
                }
                
                if(!_unknown)
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
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            LogHelper.Log($"{System.DateTime.Now} | {ErrorMessage}");
            return Task.CompletedTask;
        }
    }
}

