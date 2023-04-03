using static hypertension_bot.Data.ConnectionFactory;

namespace hypertension_bot.Models
{
    public class Configuration
    {
        /// Connessione al database
        private string _connectionString;
        private ConnectionTypes _connectionType;
        /// Token
        private string _botToken;
        private string _witToken;
        private string _gpt3Api;
        /// Query
        private string  _insertMeasures;
        private string _insertUser;
        private string _lastInsert;
        private string _calculateMonthAVG;
        private string _calculateWeekAVG;
        private string _calculateDayAVG;
        private string _getFirstAlert;
        private string _updateFirstAlert;
        private string _measurementList;
        private string _measurementAllList;
        private string _deleteMeasurement;
        private string _measurementDayList;
        private string _measurementWeekList;
        private string _measurementMonthList;
        /// Range di inserimento 
        private int    _valoreMaxSi;
        private int    _valoreMaxDi;        
        private int    _valoreMinSi;
        private int    _valoreMinDi; 
        ///Variabili per il server smtp
        private string _smtp;
        private string _username;
        private string _pwd;
        private string _recipient;
        private string _recipientUsername;
        private string _body;
        private string _subject;
        private string _attachments;
        private string _nickName;
        /// Variabili chart
        private string _chartPath;
        public string ConnectionString { get => _connectionString; set => _connectionString = value; }
        public ConnectionTypes ConnectionType { get => _connectionType; set => _connectionType = value; }
        public string BotToken { get => _botToken; set => _botToken = value; }
        public string WitToken { get => _witToken; set => _witToken = value; }
        public string GPT3Api { get => _gpt3Api; set => _gpt3Api = value; }
        public string InsertMeasures { get => _insertMeasures; set => _insertMeasures = value; }
        public string InsertUser { get => _insertUser; set => _insertUser = value; }
        public string LastInsert { get => _lastInsert; set => _lastInsert = value; }
        public string CalculateMonthAVG { get => _calculateMonthAVG; set => _calculateMonthAVG = value; }
        public string CalculateWeekAVG { get => _calculateWeekAVG; set => _calculateWeekAVG = value; }
        public string CalculateDayAVG { get => _calculateDayAVG; set => _calculateDayAVG = value; }
        public string GetFirstAlert { get => _getFirstAlert; set => _getFirstAlert = value; }
        public string UpdateFirstAlert { get => _updateFirstAlert; set => _updateFirstAlert = value; }
        public string DeleteMeasurement { get => _deleteMeasurement; set => _deleteMeasurement = value; }
        public string MeasurementList { get => _measurementList; set => _measurementList = value; }
        public string MeasurementAllList { get => _measurementAllList; set => _measurementAllList = value; }
        public string MeasurementDayList { get => _measurementDayList; set => _measurementDayList = value; }
        public string MeasurementWeekList { get => _measurementWeekList; set => _measurementWeekList = value; }
        public string MeasurementMonthList { get => _measurementMonthList; set => _measurementMonthList = value; }
        public int ValoreMaxSi { get => _valoreMaxSi; set => _valoreMaxSi = value; }
        public int ValoreMaxDi { get => _valoreMaxDi; set => _valoreMaxDi = value; }        
        public int ValoreMinSi { get => _valoreMinSi; set => _valoreMinSi = value; }
        public int ValoreMinDi { get => _valoreMinDi; set => _valoreMinDi = value; }
        public string Smtp { get => _smtp; set => _smtp = value; }
        public string Username { get => _username; set => _username = value; }
        public string Pwd { get => _pwd; set => _pwd = value; }
        public string Recipient { get => _recipient; set => _recipient = value; }
        public string RecipientUsername { get => _recipientUsername; set => _recipientUsername = value; }
        public string Body { get => _body; set => _body = value; }
        public string Subject { get => _subject; set => _subject = value; }
        public string Attachments { get => _attachments; set => _attachments = value; }
        public string NickName { get => _nickName; set => _nickName = value; }
        public string ChartPath { get => _chartPath; set => _chartPath = value; }
    }
}
