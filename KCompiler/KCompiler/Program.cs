using Common;
using PreCompiler;

namespace KCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var cli = ParseCli(args);

            //Create DI objects
            ILogger debugLogger = new DbgLog(cli.DebugLogging);
            ILogger diagLogger = new DiagLog("compiler.log");

            IPreCompiler preCompiler = new PreCompiler.PreCompiler(
                cli.PrecompTempFiles,
                debugLogger, diagLogger
                );


            //Start compilation
            preCompiler.Run(cli.SrcFile);



            //Finish
            string msg = "Finished with exit code 0";//TODO

            debugLogger.Write(msg);
            diagLogger.Write(msg);

            (diagLogger as DiagLog).Flush();
        }

        static CliOptions ParseCli(string[] args)
        {
            var ret = new CliOptions();

            foreach (var arg in args)
            {
                if (arg.Length == 1 || arg[0] != '-')
                {
                    Printer.CPrintErr($"Invalid argument '{arg}'");
                }

                if (arg[1] == '-') ParseLong(ref ret, arg);
                else ParseShort(ref ret, arg);

            }

            return ret;
        }

        static void ParseShort(ref CliOptions cli, string arg)
        {
            arg = arg.Substring(1);
            foreach (var c in arg)
            {
                switch (c)
                {
                    case 'h': PrintHelp(); break;
                    case 'w': cli.WarnAsErr = true; break;
                    case 'd': cli.DebugLogging = true; break;

                    default:

                        break;
                }
            }
        }

        static void ParseLong(ref CliOptions cli, string arg)
        {
            arg = arg.Substring(2);
            string argCom;
            string argVal;

            var split = arg.Split("=");
            argCom = split[0];
            argVal = split.Length == 2 ? split[1].ToLower() : string.Empty;

            switch (argCom)
            {
                case "arch": cli.CpuTarget = argVal; break;
                case "b64": cli.X64 = true; break;
                case "b32": cli.X64 = false; break;
                case "precomptemp": cli.PrecompTempFiles = true; break;
                case "src": cli.SrcFile = argVal; break;
                default:

                    break;
            }
        }

        static void PrintHelp()
        {

        }
    }
}
