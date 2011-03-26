using System;
using System.IO;
using System.Text;
using Moq;
using NUnit.Framework;
using Oversikt.Providers.Artifacts.Xml;
using Oversikt.Tests.Properties;
using Skaar.Oversikt.Contracts.Artifacts;

namespace Oversikt.Tests.Artifacts
{
    public class ArtifactXmlSerializerTests
    {
        private ArtifactXmlSerializer target;
        private Mock<IArtifactStream> inputMock;
        [SetUp]
        public void SetUp()
        {
            target=new ArtifactXmlSerializer();
            inputMock=new Mock<IArtifactStream>();
        }
        [Test]
        public void Deserialize_InputIsNull_Throws()
        {
            Assert.That(()=>target.Deserialize(null),Throws.TypeOf<ArgumentNullException>());
        }
        [Test]
        public void Deserialize_InputMissingDataStream_Throws()
        {
            Assert.That(() => target.Deserialize(inputMock.Object), Throws.ArgumentException);
        }
        [Test]
        public void Deserialize_ValidXml_ReturnsArtifact()
        {
            //Arrange
            var stream = new MemoryStream(Encoding.Default.GetBytes(Resources.SimpleArtifactAsXml));
            
            inputMock.Setup(i => i.Stream).Returns(stream);
            inputMock.Setup(i => i.Id).Returns(Any.String);
            
            //Act
            var result=target.Deserialize(inputMock.Object);

            //Assert
            Assert.That(result,Is.Not.Null);
            Assert.That(result.Result,Is.Not.Null);
        }
        
    }
}