using System;
using System.IO;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Oversikt.Providers.Files;
using Skaar.Oversikt.Contracts.Configuration;

namespace Oversikt.IntegrationTests.Providers.Files
{
    public class FileProviderTests
    {
        private Mock<IConfigurationProvider> config;
        private FileProvider target;
        private Mock<IFolderLocation> folderLocation;
        private readonly ConfigurationScope projectShared = ConfigurationScope.SharedProject;

        [SetUp]
        public void SetUp()
        {
            this.config = new Mock<IConfigurationProvider>();
            folderLocation = new Mock<IFolderLocation>();
            config.Setup(c => c.Get<IFolderLocation>(FileProvider.FolderLocationConfigurationKey, projectShared)).Returns(folderLocation.Object);
            target = new FileProvider(config.Object);
        }

        [Test]
        public void Get_ExistingFilePath_ReturnsStream()
        {
            using (var file = new TempFileAdapter("a"))
            {
                folderLocation.Setup(c => c.Path).Returns(file.FolderPath);

                using (var result = target.Get(file.FileName))
                {
                    Assert.That(result, Is.Not.Null);
                }
            }
        }
        [Test]
        public void Get_ExistingFilePath_ReturnsStreamWithSameContent()
        {
            using (var file = new TempFileAdapter("abc"))
            {
                folderLocation.Setup(c => c.Path).Returns(file.FolderPath);
                using (var reader = new StreamReader(target.Get(file.FileName)))
                {
                    string result = reader.ReadToEnd();
                    Assert.That(result, Is.EqualTo("abc"));
                }
            }
        }

        [Test]
        public void Get_FolderMissing_Throws()
        {
            //Arrange
            target=new FileProvider(config.Object);
            folderLocation.Setup(f => f.Path).Returns("abc");
            //Act
            ActualValueDelegate targetAction = () => target.Get("a");
            //Assert
            Assert.That(targetAction,Throws.TypeOf(typeof(DirectoryNotFoundException)));
        }
        [Test]
        public void Get_FileMissing_Throws()
        {
            using (var file = new TempFileAdapter("a"))
            {
                folderLocation.Setup(c => c.Path).Returns(file.FolderPath);

                ActualValueDelegate targetAction = () => target.Get("a");
                Assert.That(targetAction, Throws.TypeOf<FileNotFoundException>());
            }
        }

        [Test]
        public void Setup_FolderMissing_CreatesFolder()
        {
            using (var folder = new TempFolderAdapter())
            {
                target.SetupProject(folder.Path + @"\setupFolder");
                Assert.That(folder.SubFolderExists("setupFolder"));
            }
        }
        [Test]
        public void Setup_FolderExists_DoesNotCreateFolder()
        {
            using (var folder = new TempFolderAdapter())
            {
                target.SetupProject(folder.Path);
                Assert.That(folder.SubFolderExists(folder.Name),Is.False);
            }
        }
        [Test]
        public void Setup_FolderMissing_CreatesArtifactFolder()
        {
            using (var folder = new TempFolderAdapter())
            {
                target.SetupProject(folder.Path + @"\setupFolder");
                Assert.That(folder.SubFolderExists("setupFolder\\"+FileProvider.ArtifactsFolderName));
            }
        }
        [Test]
        public void Setup_FolderMissing_CreatesConfigurationFolder()
        {
            using (var folder = new TempFolderAdapter())
            {
                target.SetupProject(folder.Path + @"\setupFolder");
                Assert.That(folder.SubFolderExists("setupFolder\\"+FileProvider.ConfigurationFolderName));
            }
        }
        [Test]
        public void Setup_FolderMissing_CreatesAttachmentsFolder()
        {
            using (var folder = new TempFolderAdapter())
            {
                target.SetupProject(folder.Path + @"\setupFolder");
                Assert.That(folder.SubFolderExists("setupFolder\\"+FileProvider.AttachmentsFolderName));
            }
        }

    }
}