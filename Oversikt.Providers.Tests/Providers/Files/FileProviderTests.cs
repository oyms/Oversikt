using System;
using System.Linq;
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

        [SetUp]
        public void SetUp()
        {
            config = new Mock<IConfigurationProvider>();
            target = new FileProvider(config.Object);
        }
        [Test]
        public void Get_NoFolderPathIsSet_Throws()
        {
            ActualValueDelegate targetAction = () => target.Get(Any.String);

            Assert.That(targetAction, Throws.InvalidOperationException);
        }

        [Test]
        public void Get_IdNull_Throws()
        {
            Assert.That(()=>target.Get(null),Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Get_IdWhiteSpace_Throws()
        {
            Assert.That(() => target.Get(" "), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Get_IdEmpty_Throws()
        {
            Assert.That(() => target.Get(string.Empty), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void RequiresConfiguration_WhenCalled_ContainsFolderLocation()
        {
            var t = target as IConfigurationConsumer;
            var configurations = t.RequiresConfiguration().ToArray();
            var result = configurations.Where(c => c.Name == "Oversikt.FileProvider.EntityFileLocation" && c.Scope==ConfigurationScope.SharedProject).FirstOrDefault();
            Assert.That(result,Is.Not.Null);
        }

        [Test]
        public void Setup_PathIsEmpty_Throws()
        {
            Assert.That(() => target.SetupProject(string.Empty), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Setup_PathIsWhiteSpace_Throws()
        {
            Assert.That(() => target.SetupProject(" \t"), Throws.TypeOf<ArgumentNullException>());
        }
    }
}