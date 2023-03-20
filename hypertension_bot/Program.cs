using hypertension_bot.Data;
using hypertension_bot.Loggers;
using hypertension_bot.Models;
using hypertension_bot.Services;
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

            var receiverOptions = new ReceiverOptions { AllowedUpdates = { } };
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

            if (update.Type != UpdateType.Message) 
                return;
            if (update.Message!.Type != MessageType.Text)
                return;

            //set variables
            _data.ChatId         = update.Message.Chat.Id;
            _data.MessageText    = update.Message.Text.ToLower();
            _data.FirstName      = update.Message.From.FirstName;
            _data.Id             = (int)update.Message.From.Id;

            _dbController.InsertUser(_data.Id);

            //porte dell'inferno
            Worker worker = new Worker();
            worker.setting(botClient, cancellationToken, _data.Id, _data.ChatId);

            if (_data.MessageText.Equals("/start"))
            {
                _unknown = true;
                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                         text: $"{_data.HelloMessage.InitialMessages[_data.Random.Next(1)]}",
                                                                         cancellationToken: cancellationToken);
            }
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
                        _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                         text: $"Sei sicuro di voler eliminare la misurazione numero {num1}?" ,
                                                         cancellationToken: cancellationToken);
                    }
                }
                else
                {
                    _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                             text: $"{_data.DeleteMessage.Messages[_data.Random.Next(4)]}",
                                                                             cancellationToken: cancellationToken);
                    List<Dictionary<string, object>> list = _dbController.getMeasurementList(_data.Id);
                    var text = _data.DeleteMessage.listMessage(list);
                    _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                             text: text,
                                                                             cancellationToken: cancellationToken);
                    if(!text.Equals("Per visualizzare le misurazioni, inserirne prima una!"))
                        _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId,
                                                                                 text: "Per eliminare una misurazione indicarla nella seguente maniera:\n Ad esempio 'elimina la numero 14'",
                                                                                 cancellationToken: cancellationToken);
                }
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
            else if ((_data.NegativeMessage.Messages.Any(_data.MessageText.Contains)) && ((_data.DoneInsert) || (_data.DoneDelete)))
            {
                var t = "";
                _unknown = true;
                if (_data.DoneInsert)
                {
                    _data.DoneInsert = false;
                    t = $"{_data.ErrorMessage.Messages[_data.Random.Next(4)]}\n{_data.FirstName} prova a reinserire i dati!";
                }
                if (_data.DoneDelete)
                {
                    _data.DoneDelete = false;
                    t = $"Non preoccuparti non eliminerò alcuna misurazione!\n{_data.FirstName} prova a indicarmi nuovamente quale misurazione devo eliminare!";
                }
                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId, text: t, cancellationToken: cancellationToken);
            }
            else if ((_data.OKMessage.Messages.Any(_data.MessageText.Contains)) && ((_data.DoneInsert) || (_data.DoneDelete)))
            {
                var t = "";
                _unknown = true;
                if (_data.DoneInsert)
                {
                    _data.DoneInsert = false;
                    _dbController.InsertMeasures(_data.Diastolic, _data.Sistolic, _data.HeartRate, _data.Id);
                    _dbController.UpdateFirstAlert(_data.Id, false);
                    t = $"{_data.InsertMessage.Messages[_data.Random.Next(4)]}\nA presto {_data.FirstName}!\nData : {System.DateOnly.FromDateTime(System.DateTime.Now)}";
                }
                if (_data.DoneDelete)
                {
                    _data.DoneDelete = false;
                    var r = _dbController.DeleteMeasurement(_data.Id, _data.NumDelete);
                    t = (r) ? $"{_data.DeleteMessage.DeleteMessages[_data.Random.Next(3)]}!\nData : {System.DateOnly.FromDateTime(System.DateTime.Now)}" : "Non ho trovato la misurazione che mi hai indicato!";
                }

                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId, text: t, cancellationToken: cancellationToken);
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

                        mess = (_data.Diastolic >= 100) ? mess = mess.Remove(0, 4) : mess = "x" + mess.Substring(3);

                        success = int.TryParse(new string($"{mess}"
                                              .SkipWhile(x => !char.IsDigit(x))
                                              .TakeWhile(char.IsDigit)
                                              .ToArray()), out int num3);
                        _data.HeartRate = num3;

                        bool sistolicInRange = _data.Sistolic >= Setting.Istance.Configuration.ValoreMinSi && _data.Sistolic <= Setting.Istance.Configuration.ValoreMaxSi;
                        bool diastolicInRange = _data.Diastolic >= Setting.Istance.Configuration.ValoreMinDi && _data.Diastolic <= Setting.Istance.Configuration.ValoreMaxDi;

                        if (sistolicInRange && diastolicInRange)
                        {
                            mess = success
                                          ? $"Sistolica : {_data.Sistolic} mmHg\nDiastolica : {_data.Diastolic} mmHg\nFrequenza cardiaca : {_data.HeartRate} bpm\nSono corretti?"
                                          : $"Sistolica : {_data.Sistolic} mmHg\nDiastolica : {_data.Diastolic} mmHg\nSono corretti?";
                        }
                        else
                        {
                            mess = $"{_data.FirstName}!\n{_data.MeasuresAccepted.Message[_data.Random.Next(3)]}";
                        }

                        _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId, text: mess, cancellationToken: cancellationToken);
                    }
                }
            }
            else if (_data.AverageMessage.Messages.Any(_data.MessageText.Contains) && (_data.MessageText.Contains("medi") || _data.ListMessage.Messages.Any(_data.MessageText.Contains)))
            {
                _unknown = true;
                string responseText;

                if (_data.MessageText.Contains("mese") || _data.MessageText.Contains("mensile"))
                    responseText = (_data.MessageText.Contains("medi")) ? _data.AverageMessage.calculateMonthAVG(_data.Id, _data.FirstName) 
                                                                        : _data.ListMessage.MonthList(_data.Id, _data.FirstName);
                else if (_data.MessageText.Contains("settima"))
                    responseText = (_data.MessageText.Contains("medi")) ? _data.AverageMessage.calculateWeekAVG(_data.Id, _data.FirstName) 
                                                                        : _data.ListMessage.WeekList(_data.Id, _data.FirstName);
                else if (_data.MessageText.Contains("giorn") || _data.MessageText.Contains("oggi"))
                    responseText = (_data.MessageText.Contains("medi")) ? _data.AverageMessage.calculateDayAVG(_data.Id, _data.FirstName) 
                                                                        : _data.ListMessage.DayList(_data.Id, _data.FirstName);
                else
                    responseText = (_data.MessageText.Contains("medi")) ? $"{_data.FirstName} specifica il tipo di media che vuoi visualizzare!\nMedia giornaliera / Media mensile / Media settimanale!"
                                                                        : $"{_data.FirstName} specifica quale elenco vuoi visualizzare!\nElenco mensile / Elenco settimanale / Elenco giornaliero!";

                _data.SentMessage = await botClient.SendTextMessageAsync(chatId: _data.ChatId, text: responseText, cancellationToken: cancellationToken);
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