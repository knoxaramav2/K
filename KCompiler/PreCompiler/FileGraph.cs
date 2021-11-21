using SDK;
using System.Collections.Generic;

namespace PreCompiler
{
    class SourceFileNode : ISourceFileNode
    {
        public string Path
        {
            get { return Path; }
            private set { Path = value; }
        }
        public List<ISourceFileNode> Dependencies 
        {
            get
            {
                return Dependencies;
            }

            set
            {

            } 
        }

        public SourceFileNode(string path)
        {
            Path = path;
        }

        public ISourceFileNode AddDependent(string filePath)
        {
            var node = new SourceFileNode(filePath);


            return node;
        }
    }

    class SourceGraph : ISourceGraph
    {
        private string _mainFile;

        private ISourceFileNode _root = null;
        private ISourceFileNode _cursor = null;

        public void SetSourceFile(string mainFile)
        {
            _mainFile = mainFile;

            AddFile(null, _mainFile);
        }

        private void AddFile(string newFile, string current = null)
        {
            if (_root == null)
            {
                if (current != null)
                {
                    //TODO Throw error
                } else
                {
                    _mainFile = newFile;
                    _cursor = _root = new SourceFileNode(newFile);
                }
            } else
            {

            }
        }
    }
}
