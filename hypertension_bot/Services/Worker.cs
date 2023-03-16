using hypertension_bot.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace hypertension_bot.Services
{
    public class Worker : BackgroundService
    {
        private  ILogger<Worker> _logger;
        private static ITelegramBotClient _telegramBot;
        private CancellationToken _cancellationToken;
        private int _id;
        private long _chatId;
        private static readonly DbController _dbController = new();

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public Worker() { }

        public void setting(ITelegramBotClient telegramBot, CancellationToken token, int id, long chatId)
        {
            _telegramBot = telegramBot;
            _cancellationToken = token;
            _id = id;
            _chatId = chatId;
            _ = ExecuteAsync(_cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var _firstAlert = _dbController.GetFirstAlert(_id);
                var lastDataInsert = _dbController.LastInsert(_id);

                if (lastDataInsert != "0" && !_firstAlert)
                {
                    var n = 0;
                    n = (int.Parse(lastDataInsert) > int.Parse(System.DateTime.Today.Day.ToString()))
                                                                                                    ? int.Parse(lastDataInsert) - int.Parse(System.DateTime.Today.Day.ToString())
                                                                                                    : int.Parse(System.DateTime.Today.Day.ToString()) - int.Parse(lastDataInsert);
                    _dbController.UpdateFirstAlert(_id, false);

                    if (n > 2)
                    {
                        _dbController.UpdateFirstAlert(_id, true);
                        _ = await _telegramBot.SendTextMessageAsync(chatId: _chatId,
                                                                    text: $"E' da un po' che non prendiamo i valori!\nOggi potrebbe essere un buon giorno per farlo!",
                                                                    cancellationToken: _cancellationToken);
                    }
                }

                await Task.Delay(864000000, stoppingToken);
            }
        }

    }
}
