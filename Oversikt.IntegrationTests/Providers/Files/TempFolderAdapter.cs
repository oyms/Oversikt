using System;
using System.Diagnostics;
using System.IO;

namespace Oversikt.IntegrationTests.Providers.Files
{
    [DebuggerDisplay("{folder.FullName}")]
    class TempFolderAdapter:IDisposable
    {
        private readonly DirectoryInfo folder;

        public TempFolderAdapter()
        {
            folder = Directory.CreateDirectory(string.Format("{0}\\{1}", System.IO.Path.GetTempPath(), System.IO.Path.GetRandomFileName()));
        }
        public string Path { get { return folder.FullName; } }

        public string Name
        {
            get { return folder.Name; }
        }

        public void Dispose()
        {
            folder.Delete(true);
        }

        public bool SubFolderExists(string folderName)
        {
            return folder.GetDirectories(folderName).Length > 0;
        }
    }
}