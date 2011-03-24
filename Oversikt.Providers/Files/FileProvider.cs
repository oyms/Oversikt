using System;
using System.Diagnostics.Contracts;
using System.IO;
using Skaar.Oversikt.Contracts;
using Skaar.Oversikt.Contracts.Configuration;

namespace Oversikt.Providers.Files
{
    /// <summary>
    /// Provides loading and saving of file streams
    /// </summary>
    public class FileProvider : IPersistanceProvider
    {
        public const string FolderLocationConfigurationKey = "Oversikt.FileProvider.EntityFileLocation";
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

        #region IPersistanceProvider Members

        /// <summary>
        /// Fetches a file with the specified [relative] path
        /// </summary>
        /// <param name="id">The path to the file.</param>
        /// <returns>The file in a stream. <remarks>Closing the stream is the callers responsibility.</remarks></returns>
        /// <exception cref="InvalidOperationException">Throws exception if no folder path configuration is defined.</exception>
        public Stream Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException("id");
            IFolderLocation folderLocation = Folder;
            if (folderLocation == null || string.IsNullOrWhiteSpace(folderLocation.Path))
                throw new InvalidOperationException("The file location configuration is not specified");
            return File.OpenRead(Path.Combine(folderLocation.Path, id));
        }

        #endregion

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(configuration != null);
        }
    }
}