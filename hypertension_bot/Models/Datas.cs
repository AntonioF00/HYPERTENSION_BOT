﻿using System;
using Telegram.Bot.Types;

namespace hypertension_bot.Models
{
    public class Datas
    {
        //Message's model
        private  readonly HelloMessage _helloMessage;

        private  readonly ErrorMessage _errorMessage;

        private  readonly InsertMessage _insertMessage;

        private  readonly OkMessage _oKMessage;

        private  readonly NegativeMessage _negativeMessage;

        private  readonly PressureMessage _pressureMessage;

        private  readonly ThankMessage _thankMessage;

        private  readonly MeasuresAccepted _measuresAccepted;

        private  readonly AverageMessage _averageMessage;

        private readonly Random _rnd;

        //Block
        private int  _blockLevel;
        private bool  _messDeleted;

        //Messages and User info
        private long  _chatId;
        private string?  _messageText;
        private int  _messageId;
        private string?  _firstName;
        private string?  _lastName;
        private string _lastDataInsert;
        private long  _id;
        private Message?  _sentMessage;
        private bool _done;
        private int _diastolic;
        private int _sistolic;

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
        public string LastDataInsert { get => _lastDataInsert; set => _lastDataInsert = value; }
        public int Diastolic { get => _diastolic; set => _diastolic = value; }
        public int Sistolic { get => _sistolic; set => _sistolic = value; }

        public HelloMessage HelloMessage => new();
        public ErrorMessage ErrorMessage => new();
        public InsertMessage InsertMessage => new();
        public OkMessage OKMessage => new();
        public NegativeMessage NegativeMessage => new();
        public PressureMessage PressureMessage => new();
        public ThankMessage ThankMessage => new();
        public MeasuresAccepted MeasuresAccepted => new();
        public AverageMessage AverageMessage => new();
        public Random Random => new();

    }
}
