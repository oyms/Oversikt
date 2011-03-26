using System;
using System.Diagnostics.Contracts;
using System.Xml;
using Oversikt.Providers.Artifacts.Xml.Model;
using Skaar.Oversikt.Contracts.Artifacts;

namespace Oversikt.Providers.Artifacts.Xml
{
    public class ArtifactXmlSerializer:IArtifactSerializer
    {
        public IDeserializationResult Deserialize(IArtifactStream artifactStream)
        {
            if (artifactStream == null) throw new ArgumentNullException("artifactStream");
            if(artifactStream.Stream==null)throw new ArgumentException("The artifact iinput must contain a stream.","artifactStream");
            Contract.Ensures(Contract.Result<IDeserializationResult>()!=null);
                Contract.Ensures(Contract.Result<IDeserializationResult>().Id==artifactStream.Id);
                var stream = artifactStream.Stream;
                var positionAtStart = stream.Position;
            try
            {
                var reader = XmlReader.Create(stream);
                return new DeSerializationResult(artifactStream.Id, true, new Artifact(), new string[] {});
            }
            finally
            {
                stream.Position = positionAtStart;
            }
        }
    }
}