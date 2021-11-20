using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class FileConstants
    {
        private static FileConstants _self = null;
        private static readonly object _lock = new object();

        public string ExecPath;

        private FileConstants() 
        {
            ExecPath = AppDomain.CurrentDomain.BaseDirectory;

        }

        public static FileConstants Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_self == null) { _self = new FileConstants(); }
                    return _self;
                }
            }
        }
    }
}
