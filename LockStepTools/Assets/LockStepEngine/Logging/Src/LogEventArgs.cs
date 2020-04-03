﻿using System;

namespace LockStep.Logging
{

    public class LogEventArgs : EventArgs
    {
        public LogSeverity LogSeverity { get; }
        public string Message { get; }
        public LogEventArgs(LogSeverity logSeverity,string message)
        {
            LogSeverity = logSeverity;
            Message = message;
        }
    }
}