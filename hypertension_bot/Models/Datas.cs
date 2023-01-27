using System;
using Telegram.Bot.Types;

namespace hypertension_bot.Models
{
    public class Datas
    {
        //Block
        private int  _blockLevel;
        private bool  _messDeleted;

        //Messages and User info
        private long  _chatId;
        private string?  _messageText;
        private int  _messageId;
        private string?  _firstName;
        private string?  _lastName;
        private long  _id;
        private Message?  _sentMessage;

        private bool _done;


        public int BlockLevel { get => _blockLevel; set => _blockLevel = 0; }
        public bool MessDeleted { get => _messDeleted; set => _messDeleted = false; }
        public long ChatId { get => _chatId; set => _chatId = value; }
        public string MessageText { get => _messageText; set => _messageText = value; }
        public int MessageId { get => _messageId; set => _messageId = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public long Id { get => _id; set => _id = value; }
        public Message SentMessage { get => _sentMessage; set => _sentMessage = value; }
        public bool Done { get => _done; set => _done = value; }
    }
}
