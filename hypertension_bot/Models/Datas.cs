using Telegram.Bot.Types;

namespace hypertension_bot.Models
{
    public class Datas
    {
        //Block
        private int _blockLevel;
        private bool _messDeleted;
        private string[] _badWords;
        private string[] _veryBadWords;

        //time
        private int _year;
        private int _month;
        private int _day;
        private int _hour;
        private int _minute;
        private int _second;

        //Messages and User info
        private long _chatId;
        private string _messageText;
        private int _messageId;
        private string _firstName;
        private string _lastName;
        private long _id;
        private Message _sentMessage;

        //poll info
        private int _pollId;


        public int BlockLevel { get => _blockLevel; set => _blockLevel = 0; }
        public bool MessDeleted { get => _messDeleted; set => _messDeleted = false; }
        public string[] BadWords { get => _badWords; set =>  _badWords = new string[]{ "badword", "bad word"}; }
        public string[] VeryBadWords { get => _veryBadWords; set => _veryBadWords = new string[] { "verybadword", "very bad word" }; }
        public int Year { get => _year; set => _year = int.Parse(DateTime.UtcNow.Year.ToString()); }
        public int Month { get => _month; set => _month = int.Parse(DateTime.UtcNow.Month.ToString()); }
        public int Day { get => _day; set => _day = int.Parse(DateTime.UtcNow.Day.ToString()); }
        public int Hour { get => _hour; set => _hour = int.Parse(DateTime.UtcNow.Hour.ToString()); }
        public int Minute { get => _minute; set => _minute = int.Parse(DateTime.UtcNow.Minute.ToString()); }
        public int Second { get => _second; set => _second = int.Parse(DateTime.UtcNow.Second.ToString()); }
        public long ChatId { get => _chatId; set => _chatId = 0; }
        public string MessageText { get => _messageText; set => _messageText = value; }
        public int MessageId { get => _messageId; set => _messageId = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public long Id { get => _id; set => _id = value; }
        public Message SentMessage { get => _sentMessage; set => _sentMessage = value; }
        public int PollId { get => _pollId; set => _pollId = 0; }
    }
}
