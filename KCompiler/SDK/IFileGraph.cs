using System.Collections.Generic;

namespace SDK
{
    public interface ISourceFileNode
    {
        string Path { get; }
        List <ISourceFileNode> Dependencies { get; set; }

        ISourceFileNode AddDependent(string filePath);
    }

    public interface ISourceGraph
    {
        void SetSourceFile(string mainFile);
    } 

}
