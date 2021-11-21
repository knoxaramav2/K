using System;
using System.Collections.Generic;
using System.IO;

namespace Common
{
    public enum LogColor
    {
        Current,
        Blue,
        Green,
        Cyan,
        Red,
        Magenta,
        Yellow,
        White
    }

    public enum LogVerbosity
    {
        Quiet,
        Basic,
        Detailed
    }

    public interface ILogger
    {
        void Write(string msg, LogColor clr= LogColor.Current);
    }

    public class DbgLog : ILogger
    {
        private readonly bool _enabled = false;

        public DbgLog(bool enabled = false)
        {
            _enabled = enabled;
        }

        public void Write(string msg, LogColor clr = LogColor.Current)
        {
            if (!_enabled) { return; }

            var writeColor = Console.ForegroundColor;
            var prevColor = writeColor;

            if (clr != LogColor.Current)
            {
                switch (clr)
                {
                    case LogColor.Blue: writeColor = ConsoleColor.Blue; break;
                    case LogColor.Green: writeColor = ConsoleColor.Green; break;
                    case LogColor.Cyan: writeColor = ConsoleColor.Cyan; break;
                    case LogColor.Red: writeColor = ConsoleColor.Red; break;
                    case LogColor.Magenta: writeColor = ConsoleColor.Magenta; break;
                    case LogColor.Yellow: writeColor = ConsoleColor.Yellow; break;
                    case LogColor.White: writeColor = ConsoleColor.White; break;
                }
            }

            Console.ForegroundColor = writeColor;
            Console.WriteLine(msg);
            Console.ForegroundColor = prevColor;
        }
    }

    public class DiagLog : ILogger
    {
        private string _logPath;
        private List<string> _buffer = new List<string>();
        private int _bufferMax;

        public DiagLog(string logName, int bufferMax = 10)
        {
            _logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logName);
            _bufferMax = bufferMax;
        }

        public void Write(string msg, LogColor clr = LogColor.Current)
        {
            var timeStamp = DateTime.Now.ToString("yy-MM-dd HH:mm:ss");
            var formatted = $"{timeStamp} | {msg}";
            _buffer.Add(formatted);

            if (_buffer.Count >= _bufferMax) { Flush(); }
        }

        public void Flush()
        {
            if (_buffer.Count == 0) return;

            using (StreamWriter writer = new StreamWriter(_logPath))
            {
                foreach(var line in _buffer)
                {
                    writer.WriteLine(line);
                }
            }

            _buffer.Clear();
        }
    }
}
