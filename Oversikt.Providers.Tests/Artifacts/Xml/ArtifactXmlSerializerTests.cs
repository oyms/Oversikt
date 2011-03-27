using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Schema;
using Moq;
using NUnit.Framework;
using Oversikt.Providers.Artifacts.Xml;
using Oversikt.Tests.Artifacts.Xml.Builders;
using Oversikt.Tests.Properties;
using Skaar.Oversikt.Contracts.Artifacts;

namespace Oversikt.Tests.Artifacts.Xml
{
    public class ArtifactXmlSerializerTests
    {
        private ArtifactXmlSerializer target;
        private Mock<IArtifactStream> inputMock;
        private Stream stream; 
        [SetUp]
        public void SetUp()
        {
            target=new ArtifactXmlSerializer();
            inputMock=new Mock<IArtifactStream>();
            inputMock.Setup(i => i.Id).Returns(Any.String);
        }
        [TearDown]
        public void TearDown()
        {
            if(stream!=null)
            {
                stream.Close();
                stream = null;
            }
        }

        private void SetupSimpleArtifactStream()
        {
            SetupStream(Resources.SimpleArtifactAsXml);
        }
        private void SetupStream(string content)
        {
            stream = new MemoryStream(Encoding.Default.GetBytes(content));
            inputMock.Setup(i => i.Stream).Returns(stream);
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
            SetupSimpleArtifactStream();
            
            //Act
            var result=target.Deserialize(inputMock.Object);

            //Assert
            Assert.That(result,Is.Not.Null);
            Assert.That(result.Result,Is.Not.Null);
        }

        [Test]
        public void Deserialize_InvalidXml_ThrowsValidationException()
        {
            //Arrange
            string input = new XElement("root", new XElement("subElement", Any.String)).ToString(SaveOptions.OmitDuplicateNamespaces);
            SetupStream(input);
            
            //Assert
            Assert.That(()=>target.Deserialize(inputMock.Object),Throws.TypeOf<XmlSchemaValidationException>());
        }

        [Test]
        public void Deserialize_ValidXml_TitleIsPreserved()
        {
            //Arrange
            var xml = new ArtifactBuilder().WithTitle("a");
            SetupStream(xml.ToString());

            //Act
            var result = target.Deserialize(inputMock.Object).Result;

            //Assert
            Assert.That(result.Title, Is.EqualTo("a"));
        }
        [Test]
        public void Deserialize_ValidXml_IdIsPreserved()
        {
            //Arrange
            var xml = new ArtifactBuilder().WithId("b");
            SetupStream(xml.ToString());

            //Act
            var result = target.Deserialize(inputMock.Object).Result;

            //Assert
            Assert.That(result.Id, Is.EqualTo("b"));
        }
    }
}