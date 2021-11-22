using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using PPT = PreCompiler.PrepProcTerms;


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
        private string _rootPath;

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

            _rootPath = IsQualifiedPath(src) ?
                Path.GetDirectoryName(src) :
                AppDomain.CurrentDomain.BaseDirectory;
            _rootPath = _rootPath.ToLower();

            PreCompileFile(src);

            _diagLogger.Write("End Precompiler");

            _sourceProvider.ClearFiles();
        }

        
        private void PreCompileFile(string file)
        {
            var toImport = new List<string>();
            var contents = new List<string>();
            var filePath = Path.Combine(_rootPath, NormalizedPath(file));


            //reduce contents
            contents = File.ReadAllLines(filePath)
                .Where(x=>x.Trim().Length>0).ToList();

            ApplyPrecompRules(ref contents, ref toImport);

            foreach(var depend in toImport)
            {
                PreCompileFile(depend);
            }
        }

        private void ApplyPrecompRules(
            ref List<string>contents, 
            ref List<string>imports)
        {
            bool isCommentLine = false;
            bool isCommentMulti = false;

            foreach(var line in contents)
            {
                //comments
                var idx = FindCommentLine(line, ref isCommentLine, ref isCommentMulti);

                //preproc directives
                if (line.StartsWith(PPT.Import, StringComparison.OrdinalIgnoreCase))
                {

                }
            }
        }

        private uint FindCommentLine(string line, ref bool commLine, ref bool mlCommLine)
        {
            bool sComment = false;
            bool mComment = false;

            return 0;
        }

        private string NormalizedPath(string file)
        {
            if (!file.Contains(_rootPath))
            {
                file = Path.Combine(_rootPath, file).ToLower();
            }

            return file;
        }

        private bool IsQualifiedPath(string path)
        {
            var regex = new Regex(@"^[a-zA-Z]:\\");
            return regex.IsMatch(path);
        }
    }
}
