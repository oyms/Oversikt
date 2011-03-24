using System;
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

    }
}