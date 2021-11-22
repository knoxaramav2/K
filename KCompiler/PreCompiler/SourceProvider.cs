using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PreCompiler
{
    public struct SrcFile
    {
        public int _hash { get; private set; }
        public string _path { get; private set; }
        public List<string> _content { get; private set; }

        public SrcFile(string path, List<string> content)
        {
            _path = path;
            _content = content;
            _hash = path.GetHashCode();
        }
    }

    interface ISourceProvider
    {
        SrcFile GetFile(string path);
        void SetFile(SrcFile file);
        void ClearFiles();
    }

    //Use physical files
    class FileSourceProvider : ISourceProvider
    {
        private string _tmpDir;

        public FileSourceProvider()
        {
            _tmpDir = Path.Combine(Path.GetTempPath(), "kcmp_tmp");
        }

        public void ClearFiles()
        {
            //Directory.Delete(_tmpDir, true);
        }

        public SrcFile GetFile(string path)
        {
            var filePath = Path.Combine(_tmpDir, Path.GetFileName(path));
            var contents = File.ReadAllLines(filePath);
            var ret = new SrcFile(
                    filePath,
                    contents.ToList()
                );

            return ret;
        }

        public void SetFile(SrcFile file)
        {
            using var writer = new StreamWriter(file._path);
            foreach (var line in file._content)
            {
                writer.WriteLine(line);
            }
        }
    }

    //Use memory
    class MemorySourceProvider : ISourceProvider
    {
        public MemorySourceProvider()
        {

        }

        public void ClearFiles()
        {
            throw new System.NotImplementedException();
        }

        public SrcFile GetFile(string path)
        {
            throw new System.NotImplementedException();
        }

        public void SetFile(SrcFile file)
        {
            throw new System.NotImplementedException();
        }
    }
}
