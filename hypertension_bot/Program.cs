﻿using System;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dapper;
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
            _data.Id             = (int)update.Message.From.Id;

            _data.LastDataInsert = _dbController.LastInsert(_data.Id);
            _dbController.InsertUser(_data.Id);
            var _firstAlert = _dbController.GetFirstAlert(_data.Id);

            if (_data.MessageText.Equals("/start"))
            {
                _unknown = true;
                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                         text: $"{_data.HelloMessage.InitialMessages[_data.Random.Next(1)]}",
                                                                         cancellationToken: cancellationToken);
            }
            else if (_data.PressureMessage.Messages.Any(_data.MessageText.Contains))
            {
                _unknown = true;
                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                         text: $"{_data.PressureMessage.HowToMessages[_data.Random.Next(1)]}",
                                                                         cancellationToken: cancellationToken);
            }
            else if (_data.ThankMessage.Messages.Any(_data.MessageText.Contains))
            {
                _unknown = true;
                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                         text: $"{_data.ThankMessage.ReplyMessages[_data.Random.Next(5)]}",
                                                                         cancellationToken: cancellationToken);
            }
            else if ((_data.NegativeMessage.Messages.Any(_data.MessageText.Contains)) && (_data.Done))
            {
                _data.Done = false;
                _unknown = true;
                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                         text: $"{_data.ErrorMessage.Messages[_data.Random.Next(4)]}\n{_data.FirstName} prova a reinserire i dati!",
                                                                         cancellationToken: cancellationToken);
            }
            else if ((_data.OKMessage.Messages.Any(_data.MessageText.Contains)) && (_data.Done))
            {
                _data.Done = false;
                _unknown = true;
                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                         text: $"{_data.InsertMessage.Messages[_data.Random.Next(4)]}\nA presto {_data.FirstName}!\nData : {System.DateOnly.FromDateTime(System.DateTime.Now)}",
                                                                         cancellationToken: cancellationToken);
                _dbController.InsertMeasures(_data.Diastolic,_data.Sistolic,_data.HeartRate,_data.Id);
                _dbController.UpdateFirstAlert(_data.Id,false);

            }
            else if (_data.HelloMessage.Messages.Any(_data.MessageText.Contains))
            {
                _unknown = true;
                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                         text: $"{_data.HelloMessage.ReplyMessages[_data.Random.Next(4)]} {_data.FirstName}!",
                                                                         cancellationToken: cancellationToken);
            }
            else if (_data.MessageText.Any(char.IsDigit))
            {
                int num1, num2, num3;

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

                        _data.Sistolic  = (num1 > num2) ? num1 : num2;
                        _data.Diastolic = (num1 > num2) ? num2 : num1;

                        mess = (_data.Diastolic >= 100) ? mess = mess.Remove(0, 4) : mess = "x" + mess.Substring(3);

                        success = int.TryParse(new string($"{mess}"
                                               .SkipWhile(x => !char.IsDigit(x))
                                               .TakeWhile(x => char.IsDigit(x))
                                               .ToArray()), out num3);
                        _data.HeartRate = num3;

                        if (success)
                        {
                            if((_data.Sistolic <= Setting.Istance.Configuration.ValoreMaxSi && _data.Sistolic >= Setting.Istance.Configuration.ValoreMinSi) &&
                                (_data.Diastolic <= Setting.Istance.Configuration.ValoreMaxDi && _data.Diastolic >= Setting.Istance.Configuration.ValoreMinDi))
                            {
                                mess = $"Sistolica : {_data.Sistolic} mmHg\nDiastolica : {_data.Diastolic} mmHg\nFrequenza cardiaca : {_data.HeartRate} bpm\nSono corretti?";
                            }
                            else
                            {
                                mess = $"{_data.FirstName}!\n{_data.MeasuresAccepted.Message[_data.Random.Next(3)]}";
                            }
                        }
                        else
                        {
                            if ((_data.Sistolic <= Setting.Istance.Configuration.ValoreMaxSi && _data.Sistolic >= Setting.Istance.Configuration.ValoreMinSi) &&
                                (_data.Diastolic <= Setting.Istance.Configuration.ValoreMaxDi && _data.Diastolic >= Setting.Istance.Configuration.ValoreMinDi))
                            {
                                mess = $"Sistolica : {_data.Sistolic} mmHg\nDiastolica : {_data.Diastolic} mmHg\nSono corretti?";
                            }
                            else
                            {
                                mess = $"{_data.FirstName}!\n{_data.MeasuresAccepted.Message[_data.Random.Next(3)]}";
                            }
                        }

                        _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId, text: mess, cancellationToken: cancellationToken);
                    }
                }
            }
            else if (_data.MessageText.Contains("media") )
            {
                _unknown = true;
                string responseText;
                if (_data.MessageText.Contains("mese") || _data.MessageText.Contains("mensile"))
                    responseText = _data.AverageMessage.calculateMonthAVG(_data.Id, _data.FirstName);
                else if (_data.MessageText.Contains("settimanale") || _data.MessageText.Contains("settimana"))
                    responseText = _data.AverageMessage.calculateWeekAVG(_data.Id, _data.FirstName);
                else if (_data.MessageText.Contains("giorno") || _data.MessageText.Contains("giornaliera"))
                    responseText = _data.AverageMessage.calculateDayAVG(_data.Id, _data.FirstName);
                else
                    responseText = $"{_data.FirstName} specifica il tipo di media che vuoi visualizzare!\nMedia giornaliera / Media mensile / Media settimanale!";

                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                          text: responseText,
                                                                          cancellationToken: cancellationToken);
            }
            if (_data.LastDataInsert != "0" && !_firstAlert)
            {
                var n = 0;
                n = (int.Parse(_data.LastDataInsert) > int.Parse(System.DateTime.Today.Day.ToString())) 
                                                                                                       ? int.Parse(_data.LastDataInsert) - int.Parse(System.DateTime.Today.Day.ToString()) 
                                                                                                       : int.Parse(System.DateTime.Today.Day.ToString()) - int.Parse(_data.LastDataInsert);
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
                                                                         text: $"{_data.ErrorMessage.Messages[_data.Random.Next(6)]}",
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