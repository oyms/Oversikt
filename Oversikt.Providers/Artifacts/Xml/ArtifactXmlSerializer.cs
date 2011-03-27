using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using Oversikt.Providers.Artifacts.Xml.Model;
using Oversikt.Providers.Properties;
using Skaar.Oversikt.Contracts.Artifacts;

namespace Oversikt.Providers.Artifacts.Xml
{
    public class ArtifactXmlSerializer : IArtifactSerializer
    {
        public IDeserializationResult Deserialize(IArtifactStream artifactStream)
        {
            if (artifactStream == null) throw new ArgumentNullException("artifactStream");
            if (artifactStream.Stream == null) throw new ArgumentException("The artifact iinput must contain a stream.", "artifactStream");
            Contract.Ensures(Contract.Result<IDeserializationResult>() != null);
            Contract.Ensures(Contract.Result<IDeserializationResult>().Id == artifactStream.Id);
            var stream = artifactStream.Stream;
            var positionAtStart = stream.Position;
            try
            {
                var serializer = new DataContractSerializer(typeof(Artifact));
                var result = serializer.ReadObject(GetValidatingReader(stream)) as Artifact;
                return new DeSerializationResult(artifactStream.Id, true, result, new string[] { });
            }
            finally
            {
                stream.Position = positionAtStart;
            }
        }

        private XmlReader GetValidatingReader(Stream stream)
        {
            var settings = new XmlReaderSettings();
            settings.Schemas.Add(Schema);
            settings.ValidationEventHandler += (s,e)=>{throw e.Exception;};
            settings.ValidationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints |
                                       XmlSchemaValidationFlags.ProcessInlineSchema |
                                       XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationType = ValidationType.Schema;
            settings.CloseInput = false;
            settings.ConformanceLevel = ConformanceLevel.Auto;
            var reader = XmlReader.Create(stream,settings);
            return reader;
        }


        #region Schema

        protected XmlSchema Schema
        {
            get
            {
                if(cachedSchema==null)
                {
                    var reader = new StringReader(Resources.ArtifactSchema);
                    cachedSchema = XmlSchema.Read(reader, null);
                }
                return cachedSchema;
            }
        }
        private XmlSchema cachedSchema;

        #endregion

    }
}