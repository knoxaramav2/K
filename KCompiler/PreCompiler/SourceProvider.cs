using System.Collections.Generic;

namespace PreCompiler
{
    public struct SrcFile
    {
        private string _path;
        public List<string> _lines { get; private set; }
    }

    interface ISourceProvider
    {

    }

    class FileSourceProvider : ISourceProvider
    {
    }

    class MemorySourceProvider : ISourceProvider
    {

    }
}
