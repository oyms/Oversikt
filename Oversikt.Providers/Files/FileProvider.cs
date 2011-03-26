using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using Skaar.Oversikt.Contracts;
using Skaar.Oversikt.Contracts.Artifacts;
using Skaar.Oversikt.Contracts.Configuration;

namespace Oversikt.Providers.Files
{
    /// <summary>
    /// Provides loading and saving of file streams
    /// </summary>
    public class FileProvider : IPersistanceProvider, IConfigurationConsumer
    {
        public const string FolderLocationConfigurationKey = "Oversikt.FileProvider.EntityFileLocation";
        public const string ArtifactsFolderName = "Artifacts";
        public const string ConfigurationFolderName = "Configuration";
        public const string AttachmentsFolderName = "Attachments";
        private readonly IConfigurationProvider configuration;

        public FileProvider(IConfigurationProvider configuration)
        {
            Contract.Requires(configuration != null);
            this.configuration = configuration;
        }

        private IFolderLocation Folder
        {
            get
            {
                return configuration.Get<IFolderLocation>(FolderLocationConfigurationKey, ConfigurationScope.SharedProject);
            }
        }
        private DirectoryInfo EnsureFolder(string folderName)
        {
            IFolderLocation folderLocation = Folder;
            if (folderLocation == null || string.IsNullOrWhiteSpace(folderLocation.Path))
                throw new InvalidOperationException("The file location configuration is not specified");
            var folder = new DirectoryInfo(folderLocation.Path);
            if (!folder.Exists) throw new DirectoryNotFoundException("Project folder was not found. Please verify configuration settings.");
            var path = Path.Combine(folder.FullName, folderName);
            Contract.Assume(!string.IsNullOrEmpty(path));
            var targetFolder = new DirectoryInfo(path);
            if (!targetFolder.Exists) targetFolder = Directory.CreateDirectory(path);
            return targetFolder;
        }
        private IEnumerable<IArtifactStream> GetAll(DirectoryInfo folder)
        {
            foreach (var info in folder.EnumerateFileSystemInfos())
            {
                if(info is DirectoryInfo)
                {
                    foreach (var file in GetAll(info as DirectoryInfo))
                    {
                        yield return file;
                    }
                }
                else
                {
                    yield return new ArtifactStream(File.OpenRead(info.FullName), info.FullName);
                }
            }
        }
        #region IPersistanceProvider Members

        public IEnumerable<IArtifactStream> GetAll()
        {
            var artifactsFolder = EnsureFolder(ArtifactsFolderName);
            return GetAll(artifactsFolder);
        }

        /// <summary>
        /// Fetches a file with the specified [relative] path
        /// </summary>
        /// <param name="id">The path to the file.</param>
        /// <returns>The file in a stream. <remarks>Closing the stream is the callers responsibility.</remarks></returns>
        /// <exception cref="InvalidOperationException">Throws exception if no folder path configuration is defined.</exception>
        public Stream Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException("id");
            var folder = EnsureFolder(ArtifactsFolderName);
            return File.OpenRead(Path.Combine(folder.FullName, id));
        }

        #endregion

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(configuration != null);
        }

        /// <summary>
        /// Sets up the folders neccessary to store the project files
        /// </summary>
        /// <param name="projectPath">The folder path. The folder will be created if it does not allready exist.</param>
        public void SetupProject(string projectPath)
        {
            if (string.IsNullOrWhiteSpace(projectPath)) throw new ArgumentNullException("projectPath","Project path cannot be null or white space.");
            var baseFolder = Directory.Exists(projectPath)
                                 ? new DirectoryInfo(projectPath)
                                 : Directory.CreateDirectory(projectPath);
            baseFolder.CreateSubdirectory(ArtifactsFolderName);
            baseFolder.CreateSubdirectory(ConfigurationFolderName);
            baseFolder.CreateSubdirectory(AttachmentsFolderName);
        }

        IEnumerable<IConfigurationDefinition> IConfigurationConsumer.RequiresConfiguration()
        {
            yield return new FolderLocationDefinition();
        }

        private class FolderLocationDefinition:IConfigurationDefinition
        {
            public ConfigurationScope Scope
            {
                get { return ConfigurationScope.SharedProject; }
            }

            public ConfigurationType Type
            {
                get { return ConfigurationType.FolderPath; }
            }

            public string Name
            {
                get { return "Oversikt.FileProvider.EntityFileLocation"; }
            }

            public string Description
            {
                get { throw new NotImplementedException(); }
            }
        }
    }
}