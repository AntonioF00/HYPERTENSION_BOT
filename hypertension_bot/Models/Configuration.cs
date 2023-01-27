using static hypertension_bot.Data.ConnectionFactory;

namespace hypertension_bot.Models
{
    public class Configuration
    {
        private string _connectionString;
        private ConnectionTypes _connectionType;
        private string? _botToken;
        private string? _witToken;
        private string  _insertMeasures;

        public string ConnectionString { get => _connectionString; set => _connectionString = value; }
        public ConnectionTypes ConnectionType { get => _connectionType; set => _connectionType = value; }
        public string BotToken { get => _botToken; set => _botToken = value; }
        public string WitToken { get => _witToken; set => _witToken = value; }
        public string InsertMeasures { get => _insertMeasures; set => _insertMeasures = value; }
    }
}
