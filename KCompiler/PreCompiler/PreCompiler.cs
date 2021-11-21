using Common;
using System.IO;

namespace PreCompiler
{
    public interface IPreCompiler
    {
        void Run(string src);
    }

    public class PreCompiler : IPreCompiler
    {
        private ISourceProvider _sourceProvider;
        private ILogger _debugLogger, _diagLogger;

        public PreCompiler(bool precompTempFiles, ILogger dbg, ILogger diag)
        {
            if (precompTempFiles)
            {
                _sourceProvider = new FileSourceProvider();
            } else
            {
                _sourceProvider = new MemorySourceProvider();
            }

            _debugLogger = dbg;
            _diagLogger = diag;
        }

        public void Run(string src)
        {
            _diagLogger.Write($"Start precompile for: {Path.GetFileName(src)}");

            

            _diagLogger.Write("End Precompiler");
        }
    }
}
