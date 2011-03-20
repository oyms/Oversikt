using System.IO;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Oversikt.Providers.Files;
using Skaar.Oversikt.Contracts.Configuration;

namespace Oversikt.Tests.Providers.Files
{
    public class FileProviderTests
    {
        private Mock<IConfigurationProvider> config;
        private FileProvider target;
        private Mock<IFolderLocation> folderLocation;

        [SetUp]
        public void SetUp()
        {
            this.config = new Mock<IConfigurationProvider>();
            folderLocation=new Mock<IFolderLocation>();
        }
        [Test]
        public void Get_NoFolderPathIsSet_Throws()
        {
            target = new FileProvider(config.Object);

            ActualValueDelegate targetAction = () => target.Get(Any.String);

            Assert.That(targetAction, Throws.InvalidOperationException);
        }

        [Test]
        public void Get_ExistingFilePath_ReturnsStream()
        {
            config.Setup(c => c.Get<IFolderLocation>(FileProvider.FolderLocationConfigurationKey)).Returns(folderLocation.Object);
            var target = new FileProvider(config.Object);
            using (var file = new TempFileAdapter(Any.String))
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
            config.Setup(c => c.Get<IFolderLocation>(FileProvider.FolderLocationConfigurationKey)).Returns(folderLocation.Object);
            var target = new FileProvider(config.Object);
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
    }
}