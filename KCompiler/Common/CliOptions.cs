using System;

namespace Common
{
    public class CliOptions
    {
        public string       CpuTarget       = "AMD";
        public bool         X64             = true;
        public bool         WarnAsErr       = false;
        public LogVerbosity LogVerbosity    = LogVerbosity.Quiet;

        //Debug
        public bool     DebugLogging    = false;
    }

}
