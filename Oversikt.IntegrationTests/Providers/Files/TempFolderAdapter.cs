using System;
using System.Diagnostics;
using System.IO;

namespace Oversikt.IntegrationTests.Providers.Files
{
    [DebuggerDisplay("{Folder.FullName}")]
    class TempFolderAdapter: FolderAdapter, IDisposable
    {
        public TempFolderAdapter():base( Directory.CreateDirectory(string.Format("{0}\\{1}", System.IO.Path.GetTempPath(), System.IO.Path.GetRandomFileName())))
        {
        }

        public TempFolderAdapter(string folderName)
            : base(Directory.CreateDirectory(string.Format("{0}\\{1}", System.IO.Path.GetTempPath(), System.IO.Path.GetRandomFileName())),folderName)
        {
            
        }

        public void Dispose()
        {
            Folder.Delete(true);
        }
    }
}