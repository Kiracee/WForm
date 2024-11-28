using System;
using System.Collections.Generic;

namespace ServerApi
{

    public class Logger
    {
        internal enum LogLevel
        {
            Trace,
            Debug,
            Info,
            Warn,
            Error,
            Fatal
        }

        internal class LogEventArgs : EventArgs
        {
            public Exception Exception { get; }

            public string LoggerName { get; }

            public LogLevel LogLevel { get; }

            public string Message { get; }

            public DateTime TimeStamp { get; }

            public LogEventArgs(string loggerName, DateTime timeStamp, LogLevel logLevel, string message, Exception exception)
            {
                LoggerName = loggerName ?? throw new ArgumentNullException("loggerName");
                TimeStamp = timeStamp;
                LogLevel = logLevel;
                Message = message;
                Exception = exception;
            }
        }

        private static IDictionary<string, Logger> _loggers = new Dictionary<string, Logger>();

        private static Func<string, Action<DateTime, int, string, Exception>> _logHandlerProvider;

        public static Func<string, Action<DateTime, int, string, Exception>> LogHandlerProvider
        {
            get
            {
                return _logHandlerProvider;
            }
            set
            {
                if (!(_logHandlerProvider != value))
                {
                    return;
                }

                _logHandlerProvider = value;
                foreach (Logger value2 in _loggers.Values)
                {
                    value2.RefreshLogHandler();
                }
            }
        }

        private Action<DateTime, int, string, Exception> LogHandler { get; set; }

        private string Name { get; }

        internal static event EventHandler<LogEventArgs> EventLogged;

        private Logger(string name)
        {
            Name = name ?? throw new ArgumentNullException("name");
            RefreshLogHandler();
        }

        internal static Logger GetLogger(string name)
        {
            if (!_loggers.ContainsKey(name))
            {
                Logger logger = new Logger(name);
                _loggers.Add(logger.Name, logger);
            }

            return _loggers[name];
        }

        internal static Logger GetLoggerFor<T>()
        {
            return GetLogger(typeof(T).FullName);
        }

        internal void Debug(string message, params object[] args)
        {
            Log(LogLevel.Debug, message, args);
        }

        internal void Debug(string message, Exception exception, params object[] args)
        {
            Log(LogLevel.Debug, message, exception, args);
        }

        internal void Error(string message, params object[] args)
        {
            Log(LogLevel.Error, message, args);
        }

        internal void Error(string message, Exception exception, params object[] args)
        {
            Log(LogLevel.Error, message, exception, args);
        }

        internal void Fatal(string message, params object[] args)
        {
            Log(LogLevel.Fatal, message, args);
        }

        internal void Fatal(string message, Exception exception, params object[] args)
        {
            Log(LogLevel.Fatal, message, exception, args);
        }

        internal void Info(string message, params object[] args)
        {
            Log(LogLevel.Info, message, args);
        }

        internal void Info(string message, Exception exception, params object[] args)
        {
            Log(LogLevel.Info, message, exception, args);
        }

        internal void Log(LogLevel level, string message, Exception exception, params object[] args)
        {
            if ((LogHandler != null) | (Logger.EventLogged != null))
            {
                DateTime now = DateTime.Now;
                string text = string.Format(message, args);
                EventHandler<LogEventArgs> eventLogged = Logger.EventLogged;
                if (eventLogged != null)
                {
                    LogEventArgs e = new LogEventArgs(Name, now, level, text, exception);
                    eventLogged(null, e);
                }

                if (LogHandler != null)
                {
                    LogHandler(now, (int)level, text, exception);
                }
            }
        }

        internal void Log(LogLevel level, string message, params object[] args)
        {
            Log(level, message, null, args);
        }

        internal void Trace(string message, params object[] args)
        {
            Log(LogLevel.Trace, message, args);
        }

        internal void Trace(string message, Exception exception, params object[] args)
        {
            Log(LogLevel.Trace, message, exception, args);
        }

        internal void Warn(string message, params object[] args)
        {
            Log(LogLevel.Warn, message, args);
        }

        internal void Warn(string message, Exception exception, params object[] args)
        {
            Log(LogLevel.Warn, message, exception, args);
        }

        private void RefreshLogHandler()
        {
            if (LogHandlerProvider != null)
            {
                LogHandler = LogHandlerProvider(Name);
            }
            else
            {
                LogHandler = null;
            }
        }
    }
}
