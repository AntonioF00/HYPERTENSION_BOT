﻿using hypertension_bot.Data;
using hypertension_bot.Loggers;
using hypertension_bot.Models;
using hypertension_bot.Settings;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.IO;
using Telegram.Bot.Types.InputFiles;

namespace hypertension_bot.Services
{
    public class ResearcherWorker
    {
        private readonly Datas _data = new();
        private static readonly DbController _dbController = new();
        private readonly ITelegramBotClient _botClient;
        private NLPWorker _nlpWorker = new();
        private WitWorker _witWorker = new();
        public long _id { get; set; }

        public ResearcherWorker(long id, ITelegramBotClient botClient) 
        {
            _id = id;
            _botClient = botClient;
        }        

        public async Task<List<string>> FindResponse(long _chatId,
                                                     string _messageText, 
                                                     string _firstName,
                                                     int id)
        {
            ///variabile per determinare se il bot è stato in grado di comprendere il contesto
            bool _unknown = false;
            ///variabile che dovrà ritornare il metodo
            List<string> res = new List<string>();

            ///set variables
            _data.ChatId = _chatId;
            _data.MessageText = _messageText;
            _data.FirstName = _firstName;
            _data.Id = id; 

            /*_witWorker.Run(_messageText);
            if (!string.IsNullOrWhiteSpace(_witWorker.response.ToString()))
            {
                ///switch case
                ///verifico s'è lo /start
                ///verifico s'è un messaggio per la media
                ///verifico s'è un messaggio per la lista
                ///verifico s'è un messaggio di saluto
                ///verifico s'è un messaggio di ringraziamento
                ///verifico s'è un messaggio per come ci si misura la pressione
                ///verifico s'è un messaggio per il grafico
                ///verifico s'è un messaggio per la mail
                ///.....
            }
            else
            {
                ///....
                ///verifico s'è un messaggio per eliminare
                ///verifico s'è un inserimento
                ///verifico s'è un messaggio di accertazione o negazione
                ///nlpWorker...
            }*/

            ///messaggio iniziale /start
            if (_data.MessageText.Equals("/start"))
            {
                _unknown = true;
                res.Add($"{_data.HelloMessage.InitialMessages[_data.Random.Next(1)]}");
            }
            ///contesto di eliminazione d'una misura
            else if (_data.MessageText.Contains("elim"))
            {
                _unknown = true;
                if (_data.MessageText.Any(char.IsDigit))
                {
                    var messageText = _data.MessageText.Replace("/", "-").Replace(",", "-");
                    var digits = new string(messageText.Where(char.IsDigit).ToArray());
                    if (int.TryParse(digits, out var num1))
                    {
                        _data.DoneDelete = true;
                        _data.NumDelete = num1;
                        res.Add($"Sei sicuro di voler eliminare la misurazione numero {num1}?");
                    }
                }
                else
                {
                    res.Add($"{_data.DeleteMessage.Messages[_data.Random.Next(4)]}");
                    List<Dictionary<string, object>> list = _dbController.getMeasurementList(_data.Id);
                    var text = _data.DeleteMessage.listMessage(list);
                    res.Add(text);
                    if (!text.Equals("Per visualizzare le misurazioni, inserirne prima una!"))
                        res.Add("Per eliminare una misurazione indicarla nella seguente maniera:\n Ad esempio 'elimina la numero 14'");
                }
            }
            ///contesto d'un messaggio di ringraziamento
            else if (_data.ThankMessage.Messages.Any(_data.MessageText.Contains))
            {
                _unknown = true;
                NLPWorker _nlpWorker = new();
                await _nlpWorker.RunAsync(_data.MessageText);
                res.Add((string.IsNullOrWhiteSpace(_nlpWorker.res.ToString())) ? $"{_data.ThankMessage.ReplyMessages[_data.Random.Next(5)]}"
                                                                               : _nlpWorker.res.ToString());
            }
            ///contesto d'un messaggi di negazione dovuto alla richiesta d'inesrimento o eliminazione
            else if ((_data.NegativeMessage.Messages.Any(_data.MessageText.Contains)) && ((_data.DoneInsert) || (_data.DoneDelete)))
            {
                _unknown = true;
                if (_data.DoneInsert)
                {
                    _data.DoneInsert = false;
                    res.Add($"{_data.ErrorMessage.Messages[_data.Random.Next(4)]}\n{_data.FirstName} prova a reinserire i dati!");
                }
                if (_data.DoneDelete)
                {
                    _data.DoneDelete = false;
                    res.Add($"Non preoccuparti non eliminerò alcuna misurazione!\n{_data.FirstName} prova a indicarmi nuovamente quale misurazione devo eliminare!");
                }
            }
            ///contesto d'un messaggi di conferma dovuto alla richiesta d'inesrimento o eliminazione
            else if ((_data.OKMessage.Messages.Any(_data.MessageText.Contains)) && ((_data.DoneInsert) || (_data.DoneDelete)))
            {
                _unknown = true;
                if (_data.DoneInsert)
                {
                    _data.DoneInsert = false;
                    _dbController.InsertMeasures(_data.Diastolic, _data.Sistolic, _data.HeartRate, _data.Id);
                    _dbController.UpdateFirstAlert(_data.Id, false);
                    res.Add($"{_data.InsertMessage.Messages[_data.Random.Next(4)]}\nA presto {_data.FirstName}!\nData : {System.DateOnly.FromDateTime(System.DateTime.Now)}");
                }
                if (_data.DoneDelete)
                {
                    _data.DoneDelete = false;
                    var r = _dbController.DeleteMeasurement(_data.Id, _data.NumDelete);
                    res.Add((r) ? $"{_data.DeleteMessage.DeleteMessages[_data.Random.Next(3)]}!\nData : {System.DateOnly.FromDateTime(System.DateTime.Now)}" 
                                : "Non ho trovato la misurazione che mi hai indicato!");
                }
            }
            ///contesto d'un messaggio di saluto attualmente rimosso perchè interviene NLPworker
            else if (_data.HelloMessage.Messages.Any(_data.MessageText.Contains))
            {
                _unknown = true;
                await _nlpWorker.RunAsync(_data.MessageText);
                res.Add((string.IsNullOrWhiteSpace(_nlpWorker.res.ToString())) ? $"{_data.HelloMessage.ReplyMessages[_data.Random.Next(4)]} {_data.FirstName}!"
                                                                               : _nlpWorker.res.ToString());
            }
            ///contesto d'un messaggio di esportazione dei dati e invio via mail
            else if (_data.ExportMessage.Messages.Any(_data.MessageText.Contains))
            {
                _unknown = true;
                ///routine di invio email
                List<Dictionary<string, object>> list = _dbController.getMeasurementAllList(_data.Id);
                Setting.Istance.Configuration.Body = _data.DeleteMessage.listMessage(list);
                Setting.Istance.Configuration.Subject = $"MISURAZIONI - {_data.Id} - {_data.FirstName} | {System.DateTime.Now}";
                SmtpWorker _smtpWorker = new();
                var send = _smtpWorker.Run();
                res.Add(send ? $"{_data.ExportMessage.ReplyMessages[_data.Random.Next(3)]}!"
                             : "Qualcosa dev'essere andato storto! Riprova ad inviare piu' tardi la mail!");

            }
            ///contesto d'un messaggio di una creazione di grafici
            else if (_data.ChartMessage.Messages.Any(_data.MessageText.Contains))
            {
                _unknown = true;
                try
                {
                    ///routine di creazione grafici
                    List<Dictionary<string, object>> list = _dbController.getMeasurementMonthList(_data.Id);
                    ChartWorker _chartWorker = new ChartWorker();
                    _chartWorker._id = _data.Id;
                    _chartWorker.Run(list);
                    using (var stream = System.IO.File.OpenRead(Setting.Istance.Configuration.ChartPath + $"grafico_{_data.Id}.png"))
                    {
                        InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);
                        _ = await _botClient.SendPhotoAsync(chatId: _data.ChatId,
                                                            photo: inputOnlineFile,
                                                            allowSendingWithoutReply: true);
                    }

                    System.IO.File.Delete(Setting.Istance.Configuration.ChartPath + $"grafico_{_data.Id}.png");

                    res.Add($"{_data.ChartMessage.ReplyMessages[_data.Random.Next(2)]}!");
                }
                catch (Exception ex)
                {
                    LogHelper.Log($"{System.DateTime.Now} | Message: {ex.Message} | StackTrace: {ex.StackTrace}");
                    _unknown = false;
                }
            }
            ///contesto d'un messaggio in cui sono presenti le misure di sistolica diastolica e frequenza cardiaca
            else if (_data.MessageText.Any(char.IsDigit))
            {
                bool success = int.TryParse(new string(_data.MessageText.Replace("/", "-").Replace(",", "-")
                                    .SkipWhile(x => !char.IsDigit(x))
                                    .TakeWhile(char.IsDigit)
                                    .ToArray()), out int num1);

                if (num1 != 0 && success)
                {
                    var mess = _data.MessageText.Replace(num1.ToString(), "").Replace("/", "-").Replace(",", "-");

                    success = int.TryParse(new string($"{mess}"
                                           .SkipWhile(x => !char.IsDigit(x))
                                           .TakeWhile(char.IsDigit)
                                           .ToArray()), out int num2);
                    if (num2 != 0 && success)
                    {
                        _unknown = true;
                        _data.DoneInsert = true;

                        _data.Sistolic = (num1 > num2) ? num1 : num2;
                        _data.Diastolic = (num1 > num2) ? num2 : num1;

                        if (mess.Length > 12)
                            mess = mess.Replace(num2.ToString(), "");

                        mess = (_data.Diastolic >= 100) ? mess = mess.Remove(0, 4) : mess = "x" + mess.Substring(4);

                        success = int.TryParse(new string($"{mess}"
                                              .SkipWhile(x => !char.IsDigit(x))
                                              .TakeWhile(char.IsDigit)
                                              .ToArray()), out int num3);
                        _data.HeartRate = num3;

                        bool sistolicInRange = _data.Sistolic >= Setting.Istance.Configuration.ValoreMinSi && _data.Sistolic <= Setting.Istance.Configuration.ValoreMaxSi;
                        bool diastolicInRange = _data.Diastolic >= Setting.Istance.Configuration.ValoreMinDi && _data.Diastolic <= Setting.Istance.Configuration.ValoreMaxDi;

                        if (sistolicInRange && diastolicInRange)
                        {
                            res.Add(success
                                           ? $"Sistolica : {_data.Sistolic} mmHg\nDiastolica : {_data.Diastolic} mmHg\nFrequenza cardiaca : {_data.HeartRate} bpm\nSono corretti?"
                                           : $"Sistolica : {_data.Sistolic} mmHg\nDiastolica : {_data.Diastolic} mmHg\nSono corretti?");
                        }
                        else
                        {
                            res.Add($"{_data.FirstName}!\n{_data.MeasuresAccepted.Message[_data.Random.Next(3)]}");
                        }
                    }
                }
            }
            ///contesto su come si misura la pressione
            else if (_data.PressureMessage.Messages.Any(_data.MessageText.Contains))
            {
                _unknown = true;
                res.Add($"{_data.PressureMessage.HowToMessages[_data.Random.Next(1)]}");
            }
            ///contesto d'un messaggio per ottenere la media o una lista delle misurazioni
            else if ((_data.AverageMessage.Messages.Any(_data.MessageText.Contains) || 
                      (_data.MessageText.Contains("medio") || _data.MessageText.Contains("media"))) ||
                       _data.ListMessage.Messages.Any(_data.MessageText.Contains))
            {
                _unknown = true;

                if (_data.MessageText.Contains("mes") || _data.MessageText.Contains("mensil"))
                    res.Add((_data.MessageText.Contains("medi")) ? _data.AverageMessage.calculateMonthAVG(_data.Id, _data.FirstName)
                                                                 : _data.ListMessage.MonthList(_data.Id, _data.FirstName));
                else if (_data.MessageText.Contains("settima"))
                    res.Add((_data.MessageText.Contains("medi")) ? _data.AverageMessage.calculateWeekAVG(_data.Id, _data.FirstName)
                                                                 : _data.ListMessage.WeekList(_data.Id, _data.FirstName));
                else if (_data.MessageText.Contains("giorn") || _data.MessageText.Contains("oggi"))
                    res.Add((_data.MessageText.Contains("medi")) ? _data.AverageMessage.calculateDayAVG(_data.Id, _data.FirstName)
                                                                 : _data.ListMessage.DayList(_data.Id, _data.FirstName));
                else
                    res.Add((_data.MessageText.Contains("medi")) ? $"{_data.FirstName} specifica il tipo di media che vuoi visualizzare!\nMedia giornaliera / Media mensile / Media settimanale!"
                                                                 : $"{_data.FirstName} specifica quale elenco vuoi visualizzare!\nElenco mensile / Elenco settimanale / Elenco giornaliero!");
            }
            ///contesto d'un messaggio in cui il contesto non è stato compreso
            if (!_unknown)
            {
                ///chiamo il NLPWorker per gestire il contesto sconosciuto
                await _nlpWorker.RunAsync(_data.MessageText);
                res.Add((string.IsNullOrWhiteSpace(_nlpWorker.res.ToString())) ? $"{_data.ErrorMessage.Messages[_data.Random.Next(6)]}"
                                                                               : _nlpWorker.res.ToString());
            }
            return res;
        }
    }
}
