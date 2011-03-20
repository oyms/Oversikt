using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Oversikt.Tests.Providers.Files
{
    /// <summary>
    /// This class is used to create a temporary file.
    /// </summary>
    [DebuggerDisplay("{fileContent}")]
    class TempFileAdapter:IDisposable
    {
        private readonly string fileContent;
        private readonly string tempPath;

        public TempFileAdapter(string fileContent)
        {
            this.fileContent = fileContent;
            tempPath = Path.GetTempFileName();
            using (var writer = new StreamWriter(tempPath, false, Encoding.Default))
            {
                writer.Write(fileContent);
            }
        }

        public string FolderPath
        {
            get { return Path.GetDirectoryName(tempPath); }
        }
        public string FileName
        {
            get { return Path.GetFileName(tempPath); }
        }

        public void Dispose()
        {
            File.Delete(tempPath);
        }
    }
}