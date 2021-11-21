namespace Common
{
    public class CliOptions
    {
        public string       SrcFile;

        public string       CpuTarget       = "AMD";
        public bool         X64             = true;
        public bool         WarnAsErr       = false;
        public LogVerbosity LogVerbosity    = LogVerbosity.Quiet;
        public bool         PrecompTempFiles= false;


        //Debug
        public bool     DebugLogging    = false;
    }

}
