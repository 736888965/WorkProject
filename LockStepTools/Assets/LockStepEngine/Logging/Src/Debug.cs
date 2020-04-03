﻿using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace LockStep.Logging
{
    public class Debug
    {
        public static string prefix = "";
        static StringBuilder _traceSb = new StringBuilder();
        public static string TraceSavePath;
        public static int traceDumpLen = 128 * 1024;
        public static Stream _stream;
        public static void Trace(string msg, bool isNewLine = false, bool isNeedLogTrace = false)
        {
            if (isNewLine)
                _traceSb.AppendLine(msg);
            else
                _traceSb.Append(msg);
            if (isNeedLogTrace)
            {
                StackTrace st = new StackTrace(true);
                StackFrame[] sf = st.GetFrames();
                for (int i = 2; i < sf.Length; ++i)
                {
                    var frame = sf[i];
                    _traceSb.AppendLine(frame.GetMethod().DeclaringType.FullName + "::" + frame.GetMethod().Name);
                }
            }
            if (_traceSb.Length > traceDumpLen)
            {
                FlushTrace();
            }
        }
        public static void FlushTrace()
        {
            if (string.IsNullOrEmpty(TraceSavePath))
                return;
            if (_stream == null)
            {
                var dir = Path.GetDirectoryName(TraceSavePath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                _stream = File.Open(TraceSavePath, FileMode.OpenOrCreate, FileAccess.Write);
            }

            var bytes = UTF8Encoding.Default.GetBytes(_traceSb.ToString());
            _stream.Write(bytes, 0, bytes.Length);
            _stream.Flush();
            _traceSb.Clear();
        }

        public static void Log(string format, params object[] args)
        {
            Logger.Info(0, prefix + format, args);
        }
        public static void LogFormat(string format, params object[] args)
        {
            Logger.Info(0, prefix + format, args);
        }
        public static void LogWarning(string format, params object[] args)
        {
            Logger.Warn(0, prefix + format, args);
        }
        public static void LogError(string format, params object[] args)
        {
            Logger.Err(0, prefix + format, args);
        }
        public static void LogError(Exception e)
        {
            Logger.Err(0, prefix + e.ToString());
        }
        public static void LogErrorFormat(string format, params object[] args)
        {
            Logger.Info(0, prefix + format, args);
        }
        [Conditional("DEBUG")]
        public static void Assert(bool val, string msg = "")
        {
            Logger.Assert(0, val, prefix + msg);
        }
    }

    public class DebugInstace
    {
        private string _prefix = "";

        public DebugInstace(string prefix)
        {
            _prefix = prefix;
        }
        public void SetPrefix(string prefix)
        {
            _prefix = prefix;
        }

        public void Log(string format, params object[] args)
        {
            Logger.Info(0, _prefix + format, args);
        }
        public void LogFormat(string format, params object[] args)
        {
            Logging.Logger.Info(0, _prefix + format, args);
        }

        public void LogError(string format, params object[] args)
        {
            Logging.Logger.Err(0, _prefix + format, args);
        }

        public void LogError(Exception e)
        {
            Logging.Logger.Err(0, _prefix + e.ToString());
        }

        public void LogErrorFormat(string format, params object[] args)
        {
            Logging.Logger.Err(0, _prefix + format, args);
        }

        [Conditional("DEBUG")]
        public void Assert(bool val, string msg = "")
        {
            Logging.Logger.Assert(0, val, _prefix + msg);
        }
    }
}
