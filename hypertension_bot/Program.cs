using System;
using System.Reflection.Metadata.Ecma335;
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

        private static readonly DbController _dbController = new();

        private static Random _rnd = new();

        private static int _diastolic;

        private static int _sistolic;
        static async Task Main(string[] args)
        {
            using var _cancellationTokenSource = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };
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
            var _unknown = false;

            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Type != UpdateType.Message) 
                return;
            // Only process text messages
            if (update.Message!.Type != MessageType.Text)
                return;

            //set variables
            _data.ChatId         = update.Message.Chat.Id;
            _data.MessageText    = update.Message.Text.ToLower();
            _data.FirstName      = update.Message.From.FirstName;
            _data.Id             = update.Message.From.Id;

            _data.LastDataInsert = _dbController.LastInsert(_data.Id);
            _dbController.InsertUser(_data.Id);
            var _firstAlert = _dbController.GetFirstAlert(_data.Id);

            if (_data.ThankMessage.Messages.Contains(_data.MessageText))
            {
                _unknown = true;
                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                         text: $"{_data.ThankMessage.ReplyMessages[_rnd.Next(5)]}",
                                                                         cancellationToken: cancellationToken);
            }
            else if (_data.OKMessage.Messages.Contains(_data.MessageText) && _data.Done)
            {
                _data.Done = false;
                _unknown = true;
                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                         text: $"{_data.InsertMessage.Messages[_rnd.Next(4)]}\nA presto {_data.FirstName}!\nData : {System.DateOnly.FromDateTime(System.DateTime.Now)}",
                                                                         cancellationToken: cancellationToken);
                _dbController.InsertMeasures(_diastolic,_sistolic,_data.Id);
                _dbController.UpdateFirstAlert(_data.Id,false);

            }
            else if (_data.NegativeMessage.Messages.Contains(_data.MessageText) && _data.Done)
            {
                _data.Done = false;
                _unknown = true;
                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                         text: $"{_data.ErrorMessage.Messages[_rnd.Next(4)]}\n{_data.FirstName} prova a reinserire i dati!",
                                                                         cancellationToken: cancellationToken);
            }
            else if (_data.HelloMessage.Messages.Contains(_data.MessageText) || _data.PressureMessage.Messages.Contains(_data.MessageText))
            {
                _unknown = true;

                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                         text: $"{_data.HelloMessage.ReplyMessages[_rnd.Next(4)]} {_data.FirstName}!\n{_data.FirstName}, ti va di dirmi i tuoi valori di oggi? \nScrivimeli in questo modo\nAd esempio 120 30\nGrazie!)",
                                                                         cancellationToken: cancellationToken);
            }
            else if (_data.MessageText.Any(char.IsDigit))
            {
                int num1, num2;

                bool success = int.TryParse(new string(_data.MessageText.Replace("/", "-").Replace(",", "-")
                                            .SkipWhile(x => !char.IsDigit(x))
                                            .TakeWhile(x => char.IsDigit(x))
                                            .ToArray()), out num1);

                if (num1 != 0 && success)
                {
                    var mess = _data.MessageText.Replace(num1.ToString(), "");

                    success = int.TryParse(new string(mess
                                            .SkipWhile(x => !char.IsDigit(x))
                                            .TakeWhile(x => char.IsDigit(x))
                                            .ToArray()), out num2);

                    if (num2 != 0 && success)
                    {
                        _unknown = true;
                        _data.Done = true;

                        _sistolic  = (num1 > num2) ? num1 : num2;
                        _diastolic = (num1 > num2) ? num2 : num1;

                        _ = (_sistolic >= Setting.Istance.Configuration.ValoreMaxSi && _diastolic >= Setting.Istance.Configuration.ValoreMaxDi)  
                                                                                                                                                ? _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                                                                                                                                                           text: $"{_data.FirstName}!\n{_data.MeasuresAccepted.Message[_rnd.Next(3)]}",
                                                                                                                                                                                                           cancellationToken: cancellationToken)
                                                                                                                                                : _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                                                                                                                                                           text: $"Sistolica : {_sistolic} mmHg\nDiastolica : {_diastolic} mmHg\nSono corretti?",
                                                                                                                                                                                                           cancellationToken: cancellationToken);                     
                    }
                }
            }
            else if (_data.MessageText.Contains("media"))
            {
                _unknown = true;
                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                         text: _data.AverageMessage.calculateAVG(_data.Id, _data.FirstName),
                                                         cancellationToken: cancellationToken);
            }
            if (_data.LastDataInsert != "0" && !_firstAlert)
            {
                var n = 0;
                n = (int.Parse(_data.LastDataInsert) > int.Parse(System.DateTime.Today.Day.ToString())) ? int.Parse(_data.LastDataInsert) - int.Parse(System.DateTime.Today.Day.ToString()) : int.Parse(System.DateTime.Today.Day.ToString()) - int.Parse(_data.LastDataInsert);

                if (n > 2)
                {
                    _unknown = true;
                    _dbController.UpdateFirstAlert(_data.Id,true);
                    _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                             text: $"{_data.FirstName} è da un po' che non prendiamo i valori!\nOggi potrebbe essere un buon giorno per farlo!",
                                                                             cancellationToken: cancellationToken);
                }
            }
            if (!_unknown)
                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                         text: $"{_data.ErrorMessage.Messages[_rnd.Next(6)]}",
                                                                         cancellationToken: cancellationToken);
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
