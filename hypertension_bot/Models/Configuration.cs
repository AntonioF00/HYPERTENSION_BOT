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
        private string _insertUser;
        private string _lastInsert;
        private string _calculateMonthAVG;
        private string _calculateWeekAVG;
        private string _calculateDayAVG;
        private string _getFirstAlert;
        private string _updateFirstAlert;
        private int    _valoreMaxSi;
        private int    _valoreMaxDi;        
        private int    _valoreMinSi;
        private int    _valoreMinDi;

        public string ConnectionString { get => _connectionString; set => _connectionString = value; }
        public ConnectionTypes ConnectionType { get => _connectionType; set => _connectionType = value; }
        public string BotToken { get => _botToken; set => _botToken = value; }
        public string WitToken { get => _witToken; set => _witToken = value; }
        public string InsertMeasures { get => _insertMeasures; set => _insertMeasures = value; }
        public string InsertUser { get => _insertUser; set => _insertUser = value; }
        public string LastInsert { get => _lastInsert; set => _lastInsert = value; }
        public string CalculateMonthAVG { get => _calculateMonthAVG; set => _calculateMonthAVG = value; }
        public string CalculateWeekAVG { get => _calculateWeekAVG; set => _calculateWeekAVG = value; }
        public string CalculateDayAVG { get => _calculateDayAVG; set => _calculateDayAVG = value; }
        public int ValoreMaxSi { get => _valoreMaxSi; set => _valoreMaxSi = value; }
        public int ValoreMaxDi { get => _valoreMaxDi; set => _valoreMaxDi = value; }        
        public int ValoreMinSi { get => _valoreMinSi; set => _valoreMinSi = value; }
        public int ValoreMinDi { get => _valoreMinDi; set => _valoreMinDi = value; }
        public string GetFirstAlert { get => _getFirstAlert; set => _getFirstAlert = value; }
        public string UpdateFirstAlert { get => _updateFirstAlert; set => _updateFirstAlert = value; }
    }
}
