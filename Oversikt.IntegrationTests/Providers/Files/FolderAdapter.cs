using System.Diagnostics;
using System.IO;

namespace Oversikt.IntegrationTests.Providers.Files
{
    [DebuggerDisplay("{Path}")]
    class FolderAdapter
    {
        protected FolderAdapter(DirectoryInfo folder, string subFoldername):this(folder)
        {
            Cursor = Folder.CreateSubdirectory(subFoldername);
        }
        protected FolderAdapter(DirectoryInfo folder)
        {
            Folder = folder;
            Cursor = folder;
        }
        protected DirectoryInfo Folder { get; private set; }
        private DirectoryInfo Cursor { get; set; }
        public string Path { get { return Cursor.FullName; } }
        public string BasePath { get { return Folder.FullName; } }

        public string Name
        {
            get { return Cursor.Name; }
        }

        public bool SubFolderExists(string folderName)
        {
            return Cursor.GetDirectories(folderName).Length > 0;
        }

        public FolderAdapter AddFolder(string folderName)
        {
            return new FolderAdapter(Cursor.CreateSubdirectory(folderName));
        }
        public FolderAdapter AddFile(string fileName, string content)
        {
            using (var file = File.CreateText(Path + "\\" + fileName))
            {
                file.Write(content);
            }
            return this;
        }
    }
}