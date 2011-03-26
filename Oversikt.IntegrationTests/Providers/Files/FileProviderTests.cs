using System.IO;
using System.Linq;
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

        private void SetupFolderPathConfig(string path)
        {
            folderLocation.Setup(c => c.Path).Returns(path);
        }

        [Test]
        public void Get_ExistingFilePath_ReturnsStream()
        {
            using (var folder=new TempFolderAdapter(FileProvider.ArtifactsFolderName))
            {
                folder.AddFile("a", "b");
                SetupFolderPathConfig(folder.BasePath);
                using (var result = target.Get("a"))
                {
                    Assert.That(result, Is.Not.Null);
                }
            }
        }

        [Test]
        public void Get_ExistingFilePath_ReturnsStreamWithSameContent()
        {
            using (var folder = new TempFolderAdapter(FileProvider.ArtifactsFolderName))
            {
                folder.AddFile("a", "abc");
                SetupFolderPathConfig(folder.BasePath);
                using (var reader = new StreamReader(target.Get("a")))
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
            SetupFolderPathConfig("abc");
            //Act
            ActualValueDelegate targetAction = () => target.Get("a");
            //Assert
            Assert.That(targetAction,Throws.TypeOf(typeof(DirectoryNotFoundException)));
        }
        [Test]
        public void Get_FileMissing_Throws()
        {
            using (var folder = new TempFolderAdapter(FileProvider.ArtifactsFolderName))
            {
                SetupFolderPathConfig(folder.BasePath);

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

        [Test]
        public void GetAll_TwoFilesInArtifactsFolder_ReturnsTwoFiles()
        {
            using (var folder = new TempFolderAdapter(FileProvider.ArtifactsFolderName))
            {
                SetupFolderPathConfig(folder.BasePath);
                folder.AddFile("abc", "def").AddFile("ghi","jkl");
                bool firstFileWasFound=false, secondFileWasFound = false;
                foreach (var file in target.GetAll())using(file)
                {
                    if (file.Id == Path.Combine(folder.Path, "abc")) firstFileWasFound = true;
                    if (file.Id == Path.Combine(folder.Path, "ghi")) secondFileWasFound = true;
                }
                Assert.That(firstFileWasFound,Is.True);
                Assert.That(secondFileWasFound,Is.True);
            }
        }

        [Test]
        public void GetAll_ArtifactsFolderMissing_FolderIsCreated()
        {
            using (var folder = new TempFolderAdapter())
            {
                SetupFolderPathConfig(folder.BasePath);

                target.GetAll();

                Assert.That(folder.SubFolderExists(FileProvider.ArtifactsFolderName));
            }
        }

        [Test]
        public void GetAll_FileOutsideArtifactsFolder_NotReturned()
        {
            using (var folder = new TempFolderAdapter())
            {
                folder.AddFolder(FileProvider.ArtifactsFolderName);
                folder.AddFile("a", "b");
                SetupFolderPathConfig(folder.BasePath);

                var result = target.GetAll().ToList();

                Assert.That(result,Is.Empty);
            }
        }

        [Test]
        public void GetAll_EmptySubFolder_DoesNotReturnFolder()
        {
           using(var folder=new TempFolderAdapter(FileProvider.ArtifactsFolderName))
           {
               SetupFolderPathConfig(folder.BasePath);
               folder.AddFolder("subFolder");

               var result = target.GetAll().ToList();

               Assert.That(result, Is.Empty);
           }
        }

        [Test]
        public void GetAll_FilesInSubFolders_FilesAreReturned()
        {
            using (var folder = new TempFolderAdapter(FileProvider.ArtifactsFolderName))
            {
                SetupFolderPathConfig(folder.BasePath);
                folder.AddFile("a", "b").AddFolder("subFolder").AddFile("c", "d").AddFolder("sub sub").AddFile("e", "f");
                var result = 0;
                foreach (var artifactStream in target.GetAll())using(artifactStream)
                {
                    result++;
                }
                Assert.That(result, Is.EqualTo(3));
            }
        }
    }
}