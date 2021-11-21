using System;

namespace Common
{
    public static class Printer
    {

        public static void CPrint(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(msg);
            Console.ResetColor();
        }

        public static void CPrintL(string msg, ConsoleColor color)
        {
            CPrint(msg + Environment.NewLine, color);
        }

        public static void CPrintWarn(string msg)
        {
            CPrintL(msg, ConsoleColor.Yellow);
        }

        public static void CPrintErr(string msg)
        {
            CPrintL(msg, ConsoleColor.Red);
        }
    }
}
